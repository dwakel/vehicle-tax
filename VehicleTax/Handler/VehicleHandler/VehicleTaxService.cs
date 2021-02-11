using VehicleTax.Domain;
using VehicleTax.ViewModels;
using FluentResults;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace VehicleTax.Services
{
    public interface IVehicleTaxService
    {
        Task<Result<VehicleCategoryModel[]>> ListVehicleCategory(ListBasicViewModel query);
        Task<Result<VehicleTypeModel[]>> ListVehicleType(ListBasicViewModel query);
        Task<Result<VehicleTaxModel[]>> ListVehicleTax(ListBasicViewModel query);
        Task<Result<VehicleTaxDto[]>> ListVehicleTaxSearchSort(BasicSearchSortViewModel query);
    }
    class VehicleTaxService : IVehicleTaxService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleTaxService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<VehicleCategoryModel[]>> ListVehicleCategory(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleCategoryModel> categories = await _vehicleRepository.ListVehicleCategories(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                Limit: query.Limit).ConfigureAwait(false);

            return Result.Ok(categories.ToArray()).WithSuccess("Vehicle Categories Listed Succesfully");


        }

        public async Task<Result<VehicleTypeModel[]>> ListVehicleType(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleTypeModel> categories = await _vehicleRepository.ListVehicleType(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                VehicleCategoryId: query.VehicleCategoryId,
                Limit: query.Limit).ConfigureAwait(false);

            return Result.Ok(categories.ToArray()).WithSuccess("Vehicle Type Listed Succesfully");
        }


        public async Task<Result<VehicleTaxModel[]>> ListVehicleTax(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleTaxModel> categories = await _vehicleRepository.ListVehicleTax(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                VehicleCategoryId: query.VehicleCategoryId,
                Limit: query.Limit).ConfigureAwait(false);

            return Result.Ok(categories.ToArray()).WithSuccess("Vehicle Tax Listed Succesfully");


        }

        public async Task<Result<VehicleTaxDto[]>> ListVehicleTaxSearchSort(BasicSearchSortViewModel query)
        {
           

            IEnumerable<VehicleTaxDto> duty = await _vehicleRepository.ListVehicleTaxSearchAndSort(
                query.SearchBy, query.SortBy).ConfigureAwait(false);

            int skip = 0;
            int take = query.PerPage;
            if (query.PerPage >= 0)
                skip = (query.Page - 1) * query.PerPage;
            else
                take = duty.Count();

            return Result.Ok(duty.Skip(skip).Take(take).ToArray()).WithSuccess("Vehicle Tax Listed Succesfully");


        }

    }
}
