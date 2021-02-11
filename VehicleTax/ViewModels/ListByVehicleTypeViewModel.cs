using FluentValidation;
using FluentValidation.Results;
using System;

namespace VehicleTax.ViewModels
{
    public class ListByVehicleTypeViewModel
    {
        public ListByVehicleTypeViewModel(long? endingBefore, long? startingAfter, int limit)
        {
            EndingBefore = endingBefore;
            StartingAfter = startingAfter;
            Limit = limit;
            Result = new ListByVehicleTypeViewModelValidator().Validate(this);
        }
        public long? EndingBefore { get; }
        public long? StartingAfter { get; }
        public int Limit { get; }
        public ValidationResult Result { get; set; }
    }

    internal class ListByVehicleTypeViewModelValidator : AbstractValidator<ListByVehicleTypeViewModel>
    {
        public ListByVehicleTypeViewModelValidator()
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
