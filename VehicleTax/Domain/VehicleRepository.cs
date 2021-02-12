using VehicleTax.Data;
using VehicleTax.Settings;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTax.Domain
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleCategoryModel>> ListVehicleCategories(
            long? EndingBefore,
            long? StartingAfter,
            long Limit);
        Task<IEnumerable<VehicleTypeModel>> ListVehicleType(
            long? EndingBefore,
            long? StartingAfter,
            long? VehicleCategoryId,
            long Limit);
        Task<IEnumerable<VehicleTaxModel>> ListVehicleTax(
            long? EndingBefore,
            long? StartingAfter,
            long? VehicleCategoryId,
            long Limit);
        Task<IEnumerable<VehicleTaxDto>> ListVehicleTaxSearchAndSort(
            Dictionary<string, object> SearchBy,
            Dictionary<string, object> SortBy);

        Task<VehicleTaxDto> FetchVehicleTaxByTypeId(
            long VehicleTypeId);
    }

    internal class VehicleRepository : BaseRepository, IVehicleRepository
    {
        public VehicleRepository(IOptions<ConnectionStringSettings> options, IConnectionResolver<NpgsqlConnection> resolver) : base(options, resolver)
        {
        }
        public Task<IEnumerable<VehicleCategoryModel>> ListVehicleCategories(
            long? EndingBefore,
            long? StartingAfter,
            long Limit)
        {
            return WithConnection(conn =>
            {
                string query = "";

                if (EndingBefore != null)
                {
                    query = $@"SELECT
                                  a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                  FROM public.vehicle_category a
                                  WHERE a.id < @EndingBefore
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                              ";
                }
                else if (StartingAfter != null)
                {
                    query = $@"SELECT * FROM (
                                    SELECT
                                  a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                  FROM public.vehicle_category a
                                  WHERE a.id < @StartingAfter
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                                  
                                ) list ORDER BY id desc
                              ";
                }
                else
                {
                    query = $@"SELECT
                                  a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    null ""next""
                                  FROM public.vehicle_category a
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                                  
                              ";
                }

                return conn.QueryAsync<VehicleCategoryModel>(query, new
                {
                    EndingBefore,
                    StartingAfter,
                    Limit
                });
            });
        }


        public Task<IEnumerable<VehicleTypeModel>> ListVehicleType(
            long? EndingBefore,
            long? StartingAfter,
            long? VehicleCategoryId,
            long Limit)
        {
            return WithConnection(conn =>
            {
                string query = "";

                if (EndingBefore != null)
                {
                    query = $@"SELECT
                                  a.id, a.vehicle_category_id, a.short_name, a.description,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                  FROM public.vehicle_type a
                                  WHERE a.id < @EndingBefore
                                  {(VehicleCategoryId is null ? string.Empty : "AND a.id = @VehicleCategoryId")}
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                              ";
                }
                else if (StartingAfter != null)
                {
                    query = $@"SELECT * FROM (
                                    SELECT
                                  a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                  FROM public.vehicle_type a
                                  WHERE a.id < @StartingAfter
                                  {(VehicleCategoryId is null ? string.Empty : "AND a.id = @VehicleCategoryId")}
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                                  
                                ) list ORDER BY id desc
                              ";
                }
                else
                {
                    query = $@"SELECT
                                  a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    null ""next""
                                  FROM public.vehicle_type a
                                  {(VehicleCategoryId is null ? string.Empty : "AND a.id = @VehicleCategoryId")}
                                  ORDER BY a.id desc {(Limit >= 0 ? "LIMIT @Limit" : "")}
                                  
                              ";
                }

                return conn.QueryAsync<VehicleTypeModel>(query, new
                {
                    EndingBefore,
                    StartingAfter,
                    VehicleCategoryId,
                    Limit
                });
            });
        }


        public Task<IEnumerable<VehicleTaxModel>> ListVehicleTax(
            long? EndingBefore,
            long? StartingAfter,
            long? VehicleCategoryId,
            long Limit)
        {
            return WithConnection(conn =>
            {
                var query = "";


                //Joins slow DB so join only when needed
                string optionalJoin = default;
                if (VehicleCategoryId is not null)
                    optionalJoin = "LEFT JOIN public.vehicle_type b ON b.id = a.vehicle_type_id";

                if (EndingBefore != null)
                {
                    query = $@"SELECT
                                    a.*,
                                    LEAD(a.id) OVER(ORDER BY a.id DESC) prev,
                                    FIRST_VALUE(a.id) OVER(ORDER BY a.id DESC) ""next""
                                FROM public.vehicle_tax a
                                {optionalJoin}
                                WHERE a.id < @EndingBefore
                                {(VehicleCategoryId is null ? string.Empty : "AND b.vehicle_category_id = @VehicleCategoryId")}
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
                                    FROM public.vehicle_tax a
                                    WHERE a.id > @StartingAfter
                                    {(VehicleCategoryId is null ? string.Empty : "AND b.vehicle_category_id = @VehicleCategoryId")}
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
                                FROM public.vehicle_tax a
                                {(VehicleCategoryId is null ? string.Empty : "AND b.vehicle_category_id = @VehicleCategoryId")}
                                ORDER BY a.id DESC {(Limit >= 0 ? "LIMIT @Limit" : "")}
                              ";
                }

                return conn.QueryAsync<VehicleTaxModel>(query, new
                {
                    EndingBefore,
                    StartingAfter,
                    VehicleCategoryId,
                    Limit,
                });
            });
        }

        public Task<IEnumerable<VehicleTaxDto>> ListVehicleTaxSearchAndSort(
            Dictionary<string, object> SearchBy,
            Dictionary<string, object> SortBy)
        {
            return WithConnection(conn =>
            {
                string sortBy = "";
                string searchBy = "";

                if (SortBy != null && SortBy.Count > 0)
                {
                    string sortColumn = SortBy.ElementAt(0).Key.ToLower();
                    string value = SortBy.ElementAt(0).Value.ToString().ToLower();
                    if (value == "asc" || value == "desc")
                    {
                        //string sortValue = value;
                        sortBy = sortColumn switch
                        {
                            "importduty" => $"a.import_duty {value}",
                            "vat" => $"a.vat {value}",
                            "nhil" => $"a.nhil {value}",
                            "getfundlevy" => $"a.getfund_levy {value}",
                            "aulevy" => $"a.au_levy {value}",
                            "ecowaslevy" => $"a.ecowas_levy {value}",
                            "eximlevy" => $"a.exim_levy {value}",
                            "examlevy" => $"a.exam_levy {value}",
                            "processingfee" => $"a.processing_fee {value}",
                            "specialimportlevy" => $"a.special_import_levy {value}",
                            _ => $"a.vehicle_type_id {value}",
                        };
                        
                    }

                }

                if (SearchBy != null && SearchBy.Count > 0)
                {
                    foreach (KeyValuePair<string, object> filter in SearchBy)
                    {
                        
                        if (filter.Key.ToLower() == "importduty")
                        {
                            searchBy = $"{searchBy} AND a.import_duty = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "vat")
                        {
                            searchBy = $"{searchBy} AND a.vat = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "nhil")
                        {
                            searchBy = $"{searchBy} AND a.nhil = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "getfundlevy")
                        {
                            searchBy = $"{searchBy} AND a.getfund_levy = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "aulevy")
                        {
                            searchBy = $"{searchBy} AND a.au_levy = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "ecowaslevy")
                        {
                            searchBy = $"{searchBy} AND a.ecowas_levy = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "eximlevy")
                        {
                            searchBy = $"{searchBy} AND a.exim_levy = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "examlevy")
                        {
                            searchBy = $"{searchBy} AND a.exam_levy = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "processingfee")
                        {
                            searchBy = $"{searchBy} AND a.processing_fee = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "special_import_levy")
                        {
                            searchBy = $"{searchBy} AND a.import_duty = {Convert.ToInt64(filter.Value)}";
                        }
                        if (filter.Key.ToLower() == "categoryname")
                        {
                            searchBy = $"{searchBy} AND c.short_name =  '%{filter.Value.ToString().ToLower()}%'";
                        }
                        if (filter.Key.ToLower() == "categorydescription")
                        {
                            searchBy = $"{searchBy} AND c.description =  '%{filter.Value.ToString().ToLower()}%'";
                        }
                        if (filter.Key.ToLower() == "typename")
                        {
                            searchBy = $"{searchBy} AND b.short_name =  '%{filter.Value.ToString().ToLower()}%'";
                        }
                        if (filter.Key.ToLower() == "typedescription")
                        {
                            searchBy = $"{searchBy} AND b.description =  '%{filter.Value.ToString().ToLower()}%'";
                        }

                    }
                }

                string query = "";
               
                    query = $@"SELECT
                                    a.*, b.short_name vehicle_type_name, b.description vehicle_type_description,
                                    c.short_name vehicle_category_name, c.description vehicle_category_description
                                FROM public.vehicle_tax a
                                LEFT JOIN public.vehicle_type b ON b.id = a.vehicle_type_id
                                LEFT JOIN public.vehicle_category c ON c.id = b.vehicle_category_id
                                {searchBy}
                                ORDER BY {(string.IsNullOrWhiteSpace(sortBy) ? "a.vehicle_type_id ASC" : sortBy)}
                              ";
                

                return conn.QueryAsync<VehicleTaxDto>(query, new
                {
                    SearchBy,
                    SortBy
                });
            });
        }


        public Task<VehicleTaxDto> FetchVehicleTaxByTypeId(
            long VehicleTypeId)
        {
            return WithConnection(conn =>
            {
                

                string query = "";

                query = $@"SELECT
                                    a.*, b.short_name vehicle_type_name, b.description vehicle_type_description,
                                    c.short_name vehicle_category_name, c.description vehicle_category_description
                                FROM public.vehicle_tax a
                                LEFT JOIN public.vehicle_type b ON b.id = a.vehicle_type_id
                                LEFT JOIN public.vehicle_category c ON c.id = b.vehicle_category_id
                                WHERE b.id = @VehicleTypeId
                              ";


                return conn.QuerySingleOrDefaultAsync<VehicleTaxDto>(query, new
                {
                    VehicleTypeId
                });
            });
        }
    }
}
