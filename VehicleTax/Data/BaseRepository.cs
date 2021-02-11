using VehicleTax.Settings;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTax.Data
{
    public abstract class BaseRepository
    {
        private readonly ConnectionStringSettings _connectionString;
        private readonly IConnectionResolver<NpgsqlConnection> _resolver; //to use multipe or different provider simple dependency inject new provide

        protected BaseRepository(IOptions<ConnectionStringSettings> options, IConnectionResolver<NpgsqlConnection> resolver)
        {
            _connectionString = options.Value;
            _resolver = resolver;
        }

        private async Task<IDbConnection> GetConnection()
        {
            return await _resolver.Resolve(_connectionString.Default).ConfigureAwait(false);
        }

        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            var connection = await GetConnection().ConfigureAwait(false);

            // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
            return await getData(connection).ConfigureAwait(false);
        }

        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            var connection = await GetConnection().ConfigureAwait(false);

            // Asynchronously execute getData, which has been passed in as a Func<IDBConnection, Task<T>>
            await getData(connection).ConfigureAwait(false);
        }
    }
}
