using FluentValidation;
using FluentValidation.Results;
using System;

namespace VehicleTax.ViewModels
{
    public class ListBasicViewModel
    {
        public ListBasicViewModel(long? endingBefore, long? startingAfter, int limit, long? vehicleCategoryId = null)
        {
            EndingBefore = endingBefore;
            StartingAfter = startingAfter;
            Limit = limit;
            Result = new ListBasicViewModelValidator().Validate(this);
        }
        public long? EndingBefore { get; }
        public long? StartingAfter { get; }
        public int Limit { get; }
        public long? VehicleCategoryId { get; set; }
        public ValidationResult Result { get; set; }
    }

    internal class ListBasicViewModelValidator : AbstractValidator<ListBasicViewModel>
    {
        public ListBasicViewModelValidator()
        {

            RuleFor(request => request.Limit)
                .LessThanOrEqualTo(50);
            
            RuleFor(request => request.StartingAfter)
                .Null()
                .When(r => r.EndingBefore != null)
                .WithMessage("'StartingAfter' cannot be specified when 'EndingBefore' is specified");
        }

    }

}
