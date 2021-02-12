using FluentResults;
using NUnit.Framework;
using System.Linq;
using VehicleTax.Domain;
using VehicleTax.Services;
using VehicleTax.ViewModels;

namespace VehicleTax.Test.ViewModels
{
    class CalculateDutyViewModelTest
    {
        private VehicleTaxDto _vehicleTax;
        private readonly double CIF = 100000;
        private readonly long _vehicleTypeId = 1100;


        [SetUp]
        public void Setup()
        {
            _vehicleTax = new VehicleTaxDto()
            {
                Id = 2,
                VehicleTypeId = 1100,
                ImportDuty = 0.2,
                Vat = 0.125,
                Nhil = 0.025,
                GetfundLevy = 0.025,
                AuLevy = 0.02,
                EcowasLevy = 0.05,
                EximLevy = 0.075,
                ExamLevy = 0.01,
                ProcessingFee = 0,
                SpecialImportLevy = 0.02
            };
        }

        [Test]
        public void CalculateDuty_InvalidTypeIdValidCIFInput_ValidationError()
        {

            CalculateDutyViewModel viewModel = new CalculateDutyViewModel(
                vehicleTypeId: 0,
                cIF: CIF);

            Assert.IsFalse(viewModel.Result.IsValid);

        }

        [Test]
        public void CalculateDuty_ValidTypeAndCIFIdInput_ValidationSuccess()
        {
            CalculateDutyViewModel viewModel = new CalculateDutyViewModel(
                vehicleTypeId: _vehicleTypeId,
                cIF: CIF);

            Assert.IsTrue(viewModel.Result.IsValid);

        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(-9111)]
        public void CalculateDuty_ValidCIFInputNegativeTypeID_ValidationSuccess(long value)
        {
            CalculateDutyViewModel viewModel = new CalculateDutyViewModel(
                vehicleTypeId: value,
                cIF: CIF);

            Assert.IsFalse(viewModel.Result.IsValid);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(-9111)]
        public void CalculateDuty_ValidInputTypeIDNegativeCIF_ValidationSuccess(long value)
        {
            CalculateDutyViewModel viewModel = new CalculateDutyViewModel(
                vehicleTypeId: _vehicleTypeId,
                cIF: value);

            Assert.IsFalse(viewModel.Result.IsValid);
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(-9111)]
        public void CalculateDuty_NegativeTypeIdNegativeCIF_ValidationSuccess(long value)
        {
            CalculateDutyViewModel viewModel = new CalculateDutyViewModel(
                vehicleTypeId: value,
                cIF: value);

            Assert.IsFalse(viewModel.Result.IsValid);
        }

    }
}
