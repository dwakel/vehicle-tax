using System.Collections.Generic;
namespace VehicleTax.ViewModels
{
    public class SearchSortRequest
    {
        public Dictionary<string, object> SearchBy { get; set; }
        public Dictionary<string, object> SortBy { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}