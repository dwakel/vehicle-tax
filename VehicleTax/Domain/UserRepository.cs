using VehicleTax.Data;
using VehicleTax.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTax.Domain
{
    public interface IUserRepository
    {
        Task<VehicleTaxModel> FindUserByEmail(string Email);
        Task<VehicleTaxModel> AddUser(
           string Name,
           string Email,
           string Password,
           string Description,
           string PhoneNumber,
           string Gender,
           bool IsActive);

        Task<VehicleTaxModel> UpdateUser(
            long UserId,
            string Name,
            string Description,
            string PhoneNumber,
            string Gender);

        Task<VehicleTaxModel> UpdateUserEmail(
            long UserId,
            string Email);

        Task<VehicleTaxModel> UpdateUserPassword(
            long UserId,
            string Password);

        Task<IEnumerable<VehicleTaxModel>> ListUsers(
            long? EndingBefore,
            long? StartingAfter,
            long Limit);
    }

    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IOptions<ConnectionStringSettings> options, IConnectionResolver<NpgsqlConnection> resolver) : base(options, resolver)
        {
        }
        public Task<VehicleTaxModel> FindUserByEmail(string Email)
        {
            return WithConnection(conn =>
            {
                const string query = @"SELECT
                                  a.*
                                  FROM public.user a
                                  WHERE LOWER(a.email) = LOWER(@Email)
                              ";

                return conn.QuerySingleOrDefaultAsync<VehicleTaxModel>(query, new
                {
                    Email = Email?.ToLowerInvariant(),
                });
            });
        }

        public Task<VehicleTaxModel> AddUser(
           string Name,
           string Email,
           string Password,
           string Description,
           string PhoneNumber,
           string Gender,
           bool IsActive)
        {
            return WithConnection(conn =>
            {
                const string query = @"
                                INSERT INTO public.user
                                (
                                  name,
                                  email,
                                  password,
                                  description,
                                  phone_number,
                                  gender,
                                  is_active
                                )
                                VALUES
                                (
                                  @Name,
                                  @Email,
                                  @Password,
                                  @Description,
                                  @PhoneNumber,
                                  @Gender,
                                  @IsActive
                                )
                                RETURNING *;
                        ";

                return conn.QuerySingleAsync<VehicleTaxModel>(query, new
                {
                    Name,
                    Email,
                    Password,
                    Description,
                    PhoneNumber,
                    Gender,
                    IsActive
                });
            });
        }




        public Task<VehicleTaxModel> UpdateUser(
            long UserId,
            string Name,
            string Description,
            string PhoneNumber,
            string Gender)
        {
            return WithConnection(conn =>
            {
                const string query = @"
                                UPDATE public.user SET
                                  name = @Name,
                                  description = @Description,
                                  phone_number = @PhoneNumber,
                                  gender = @Gender,
                                WHERE id = @UserId
                                RETURNING *;
                        ";

                return conn.QuerySingleAsync<VehicleTaxModel>(query, new
                {
                    UserId,
                    Name,
                    Description,
                    PhoneNumber,
                    Gender
                });
            });
        }

       
        public Task<VehicleTaxModel> UpdateUserEmail(
            long UserId,
            string Email)
        {
            return WithConnection(conn =>
            {
                const string query = @"
                                UPDATE public.user SET
                                  email = @Email
                                WHERE id = @UserId
                                RETURNING *;
                        ";

                return conn.QuerySingleAsync<VehicleTaxModel>(query, new
                {
                    UserId,
                    Email,
                });
            });
        }

        public Task<VehicleTaxModel> UpdateUserPassword(
            long UserId,
            string Password)
        {
            return WithConnection(conn =>
            {
                const string query = @"
                                UPDATE public.user SET
                                  password = @Password
                                WHERE id = @UserId
                                RETURNING *;
                        ";

                return conn.QuerySingleAsync<VehicleTaxModel>(query, new
                {
                    UserId,
                    Password,
                });
            });
        }

        public Task<IEnumerable<VehicleTaxModel>> ListUsers(
            long? EndingBefore,
            long? StartingAfter,
            long Limit)
        {
            return WithConnection(conn =>
            {
                var query = "";

                if (EndingBefore != null)
                {
                    query = $@"SELECT
                                    a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                FROM public.user a
                                WHERE a.id < @EndingBefore
                                ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                              ";
                }
                else if (StartingAfter != null)
                {
                    query = $@"SELECT * FROM (
                                    SELECT
                                        a.*,
                                        LAST_VALUE(a.id) OVER(ORDER BY a.id ASC) prev,
                                        LEAD(a.id) OVER(ORDER BY a.id ASC) ""next""
                                    FROM public.user a
                                    WHERE a.id > @StartingAfter
                                    ORDER BY a.id ASC {(Limit >= 0 ? "LIMIT @Limit" : "")}
                                ) list ORDER BY id desc
                              ";
                }
                else
                {
                    query = $@"SELECT
                                    a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    null ""next""
                                FROM public.user a
                                ORDER BY a.id DESC {(Limit >= 0 ? "LIMIT @Limit" : "")}
                              ";
                }

                return conn.QueryAsync<VehicleTaxModel>(query, new
                {
                    EndingBefore,
                    StartingAfter,
                    Limit,
                });
            });
        }
    }
}
