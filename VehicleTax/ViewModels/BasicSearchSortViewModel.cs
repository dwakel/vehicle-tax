using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace VehicleTax.ViewModels
{
    public class BasicSearchSortViewModel
    {
        public BasicSearchSortViewModel(Dictionary<string, object> searchBy, Dictionary<string, object> sortBy, int page = 0, int perPage = 0)
        {
            SearchBy = searchBy;
            SortBy = sortBy;
            Page = page;
            PerPage = perPage;
            Result = new BasicSearchSortViewModelValidator().Validate(this);
        }
        public Dictionary<string, object> SearchBy { get; }
        public Dictionary<string, object> SortBy { get; }
        public int Page { get; }
        public int PerPage { get; }
        public ValidationResult Result { get; set; }
    }

    internal class BasicSearchSortViewModelValidator : AbstractValidator<BasicSearchSortViewModel>
    {
        public BasicSearchSortViewModelValidator()
        {

            RuleFor(request => request.PerPage)
                .LessThanOrEqualTo(1000);

            RuleFor(request => request.Page)
                .NotNull();
        }

    }

}
