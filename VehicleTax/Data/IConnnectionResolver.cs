using System.Data;
using System.Threading.Tasks;

namespace VehicleTax.Data
{
    public interface IConnectionResolver<T> where T : IDbConnection
    {
        Task<T> Resolve(string connectionString);
    }
}
