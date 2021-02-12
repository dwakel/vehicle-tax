using FluentValidation;
using FluentValidation.Results;

namespace VehicleTax.ViewModels
{
    public class CalculateDutyViewModel
    {
        public CalculateDutyViewModel(long vehicleTypeId, double cIF)
        {
            VehicleTypeId = vehicleTypeId;
            CIF = cIF;
            Result = new CalculateDutyViewModelValidator().Validate(this);
        }

        public long VehicleTypeId { get; set; }
        public double CIF { get; set; }
        public ValidationResult Result { get; set; }
    }

    internal class CalculateDutyViewModelValidator : AbstractValidator<CalculateDutyViewModel>
    {
        public CalculateDutyViewModelValidator()
        {
            RuleFor(request => request.CIF)
                .NotNull().GreaterThan(0);
            RuleFor(request => request.VehicleTypeId)
                .NotEmpty().GreaterThan(0);
        }

    }

}
