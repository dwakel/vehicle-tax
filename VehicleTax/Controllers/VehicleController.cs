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
        private readonly IVehicleTaxService _vehicleTaxService;

        public VehicleController(ILogger<VehicleController> logger, IVehicleTaxService vehicleTaxService)
        {
            _logger = logger;
            _vehicleTaxService = vehicleTaxService;
        }


        [HttpGet("Categories")]
        public async Task<ActionResult<ListResponse<VehicleCategoryModel>>> ListCategories(
            [FromQuery] long? EndingBefore = null,
            [FromQuery] long? StartingAfter = null,
            [FromQuery] int Limit = 10)
        {
            Result<VehicleCategoryModel[]> res = await _vehicleTaxService.ListVehicleCategory(
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

        [HttpGet("Types")]
        public async Task<ActionResult<ListResponse<VehicleTypeModel>>> ListTypes(
            [FromQuery] long? EndingBefore = null,
            [FromQuery] long? StartingAfter = null,
            [FromQuery] int Limit = 10)
        {
            Result<VehicleTypeModel[]> res = await _vehicleTaxService.ListVehicleType(
                new ListBasicViewModel(
                    endingBefore: EndingBefore,
                    startingAfter: StartingAfter,
                    limit: Limit));

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


        [HttpGet("Tax")]
        public async Task<ActionResult<ListResponse<VehicleTaxModel>>> ListTax(
           [FromQuery] long? EndingBefore = null,
           [FromQuery] long? StartingAfter = null,
           [FromQuery] int Limit = 10)
        {
            Result<VehicleTaxModel[]> res = await _vehicleTaxService.ListVehicleTax(
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
        [HttpPost("Tax/SearchSort")]
        public async Task<ActionResult<ListResponse<VehicleTaxDto>>> ListTax(
           [FromBody] SearchSortRequest searchSortRequest)
        {
            Result<VehicleTaxDto[]> res = await _vehicleTaxService.ListVehicleTaxSearchSort(
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
    }
}
