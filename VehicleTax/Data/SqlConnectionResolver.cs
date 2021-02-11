using Npgsql;
using System;
using System.Collections.Concurrent;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace VehicleTax.Data
{
    internal class SqlConnectionResolver : IConnectionResolver<NpgsqlConnection>, IDisposable
    {
        private readonly ConcurrentDictionary<string, NpgsqlConnection> _connections = new ConcurrentDictionary<string, NpgsqlConnection>();

        public async Task<NpgsqlConnection> Resolve(string connectionString)
        {
            var conStr = connectionString?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(connectionString));
            var enlist = conStr.Contains("enlist=true");

            if (!enlist && !conStr.Contains("enlist="))
            {
                throw new ArgumentException("Connection string does specify explicit Enlistment requirements.");
            }
            else if (!enlist && !conStr.Contains("enlist=false"))
            {
                throw new ArgumentException("Unable to determine Enlistment requirements.");
            }

            if (!_connections.ContainsKey(conStr))
            {
                var conn = new NpgsqlConnection(connectionString);

                if (enlist)
                {
                    // net core does not support DTC yet so we have to ensure we meet the requirements for the LTM by using a single connection
                    // we want to open the connection so it remains open after dapper uses it
                    await conn.OpenAsync().ConfigureAwait(false);
                }
                _connections[conStr] = conn;
            }

            if (enlist && Transaction.Current != null)
            {
                _connections[conStr].EnlistTransaction(Transaction.Current);
            }

            return _connections[conStr];
        }

        #region IDisposable Support

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    foreach (var conn in _connections)
                    {
                        conn.Value.Dispose();
                    }
                    _connections.Clear();
                }

                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }

        #endregion IDisposable Support
    }
}
