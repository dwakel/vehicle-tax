using VehicleTax.Domain;
using VehicleTax.ViewModels;
using FluentResults;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace VehicleTax.Services
{
    public interface IVehicleTaxHandler
    {
        Task<Result<VehicleCategoryModel[]>> ListVehicleCategory(ListBasicViewModel query);
        Task<Result<VehicleTypeModel[]>> ListVehicleType(ListBasicViewModel query);
        Task<Result<VehicleTaxModel[]>> ListVehicleTax(ListBasicViewModel query);
        Task<Result<VehicleTaxDto[]>> ListVehicleTaxSearchSort(BasicSearchSortViewModel query);
        Task<Result<double>> CalculateDuty(CalculateDutyViewModel query);
    }
    class VehicleTaxHandler : IVehicleTaxHandler
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<VehicleTaxHandler> _logger;
        public VehicleTaxHandler(IVehicleRepository vehicleRepository, ILogger<VehicleTaxHandler> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public async Task<Result<VehicleCategoryModel[]>> ListVehicleCategory(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                _logger.LogError($"Validation failed: {query.Result.Errors.FirstOrDefault()}");
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleCategoryModel> categories = await _vehicleRepository.ListVehicleCategories(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                Limit: query.Limit).ConfigureAwait(false);

            _logger.LogInformation("Vehicle Categories Listed Succesfully");
            return Result.Ok(categories.ToArray())
                .WithSuccess("Vehicle Categories Listed Succesfully");


        }

        public async Task<Result<VehicleTypeModel[]>> ListVehicleType(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                _logger.LogError($"Validation failed: {query.Result.Errors.FirstOrDefault().ToString()}");
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleTypeModel> types = await _vehicleRepository.ListVehicleType(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                VehicleCategoryId: query.VehicleCategoryId,
                Limit: query.Limit).ConfigureAwait(false);

            _logger.LogInformation("Vehicle Type Listed Succesfully");
            return Result.Ok(types.ToArray())
                .WithSuccess("Vehicle Type Listed Succesfully");
        }


        public async Task<Result<VehicleTaxModel[]>> ListVehicleTax(ListBasicViewModel query)
        {
            if (!query.Result.IsValid)
            {
                _logger.LogError($"Validation failed: {query.Result.Errors.FirstOrDefault().ToString()}");
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            IEnumerable<VehicleTaxModel> taxes = await _vehicleRepository.ListVehicleTax(
                EndingBefore: query.EndingBefore,
                StartingAfter: query.StartingAfter,
                VehicleCategoryId: query.VehicleCategoryId,
                Limit: query.Limit).ConfigureAwait(false);


            _logger.LogInformation("Vehicle Tax Listed Succesfully");
            return Result.Ok(taxes.ToArray())
                .WithSuccess("Vehicle Tax Listed Succesfully");


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


            _logger.LogInformation("Vehicle Tax Listed Succesfully");
            return Result.Ok(duty.Skip(skip).Take(take).ToArray())
                .WithSuccess("Vehicle Tax Listed Succesfully");


        }

        public async Task<Result<double>> CalculateDuty(CalculateDutyViewModel query)
        {
            if (!query.Result.IsValid)
            {
                _logger.LogError($"Validation failed: {query.Result.Errors.FirstOrDefault().ToString()}");
                return Result.Fail(new Error(query.Result.Errors.FirstOrDefault().ToString()));
            }

            VehicleTaxDto taxInfo = await _vehicleRepository.FetchVehicleTaxByTypeId(query.VehicleTypeId).ConfigureAwait(false);

            if (taxInfo is null)
            {
                _logger.LogError($"Vehicle Type does not exist: {query.Result.Errors.FirstOrDefault().ToString()}");
                return Result.Fail(new Error("Vehicle Type does not exist!"));
            }


            return Result.Ok(taxInfo.CalculateDuty(query.CIF))
                .WithSuccess("Vehicle Tax Listed Succesfully");


        }

    }
}
