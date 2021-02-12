using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using VehicleTax.Services;
using VehicleTax.ViewModels;
using System.Threading.Tasks;
using FluentResults;
using VehicleTax.Response;
using VehicleTax.Domain;

namespace VehicleTax.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleTaxHandler _vehicleTaxHandler;

        public VehicleController(ILogger<VehicleController> logger, IVehicleTaxHandler vehicleTaxHandler)
        {
            _logger = logger;
            _vehicleTaxHandler = vehicleTaxHandler;
        }


        [HttpGet("VehicleCategories")]
        public async Task<ActionResult<ListResponse<VehicleCategoryModel>>> ListCategories(
            [FromQuery] long? EndingBefore = null,
            [FromQuery] long? StartingAfter = null,
            [FromQuery] int Limit = 10)
        {
            Result<VehicleCategoryModel[]> res = await _vehicleTaxHandler.ListVehicleCategory(
                new ListBasicViewModel(
                    endingBefore: EndingBefore,
                    startingAfter: StartingAfter,
                    limit: Limit));

            if (res.IsSuccess)
            {
                return new ListResponse<VehicleCategoryModel>
                {
                    Data = res.Value,
                    PreviousCursor = res.Value.LastOrDefault()?.PreviousCursor,
                    NextCursor = res.Value.FirstOrDefault()?.NextCursor,
                    TotalRecords = res.Value.Length

                };
            }

            return BadRequest(new { Error = res.Errors.FirstOrDefault() });
        }


        [HttpGet("VehicleTypes")]
        public async Task<ActionResult<ListResponse<VehicleTypeModel>>> ListTypes(
            [FromQuery] long? EndingBefore = null,
            [FromQuery] long? StartingAfter = null,
            [FromQuery] long? VehicleCategoryId = null,
            [FromQuery] int Limit = 10)
        {
            Result<VehicleTypeModel[]> res = await _vehicleTaxHandler.ListVehicleType(
                new ListBasicViewModel(
                    endingBefore: EndingBefore,
                    startingAfter: StartingAfter,
                    limit: Limit,
                    vehicleCategoryId: VehicleCategoryId));

            if (res.IsSuccess)
            {
                return new ListResponse<VehicleTypeModel>
                {
                    Data = res.Value,
                    PreviousCursor = res.Value.LastOrDefault()?.PreviousCursor,
                    NextCursor = res.Value.FirstOrDefault()?.NextCursor,
                    TotalRecords = res.Value.Length

                };
            }

            return BadRequest(new { Error = res.Errors.FirstOrDefault() });
        }


        [HttpGet("TaxInformation")]
        public async Task<ActionResult<ListResponse<VehicleTaxModel>>> ListTax(
           [FromQuery] long? EndingBefore = null,
           [FromQuery] long? StartingAfter = null,
           [FromQuery] int Limit = 10)
        {
            Result<VehicleTaxModel[]> res = await _vehicleTaxHandler.ListVehicleTax(
                new ListBasicViewModel(
                    endingBefore: EndingBefore,
                    startingAfter: StartingAfter,
                    limit: Limit));

            if (res.IsSuccess)
            {
                return new ListResponse<VehicleTaxModel>
                {
                    Data = res.Value,
                    PreviousCursor = res.Value.LastOrDefault()?.PreviousCursor,
                    NextCursor = res.Value.FirstOrDefault()?.NextCursor,
                    TotalRecords = res.Value.Length
                };
            }

            return BadRequest(new { Error = res.Errors.FirstOrDefault() });
        }
        

        [HttpPost("TaxInformation/SearchSort")]
        public async Task<ActionResult<ListResponse<VehicleTaxDto>>> ListTax(
           [FromBody] SearchSortRequest searchSortRequest)
        {
            Result<VehicleTaxDto[]> res = await _vehicleTaxHandler.ListVehicleTaxSearchSort(
            new BasicSearchSortViewModel(
                searchBy: searchSortRequest.SearchBy,
                sortBy: searchSortRequest.SortBy,
                page: searchSortRequest.Page,
                perPage: searchSortRequest.PerPage));

            if (res.IsSuccess)
            {
                return new ListResponse<VehicleTaxDto>
                {
                    Data = res.Value,
                    TotalRecords = res.Value.Length
                };
            }

            return BadRequest(new { Error = res.Errors.FirstOrDefault() });
        }

        [HttpGet("VehicleType/{VehicleTypeId}/CalculateDuty")]
        public async Task<ActionResult<double>> CalculateDuty(
           [FromRoute] long VehicleTypeId,
           [FromQuery] double CIF)
        {
            Result<double> res = await _vehicleTaxHandler.CalculateDuty(
                new CalculateDutyViewModel(
                    vehicleTypeId: VehicleTypeId,
                    cIF: CIF));
            if (res.IsSuccess)
            {
                return Ok(res.Value);
            }

            return BadRequest(new { Error = res.Errors.FirstOrDefault() });
        }
    }
}
