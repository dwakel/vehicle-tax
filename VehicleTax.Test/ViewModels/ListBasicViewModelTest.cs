using FluentResults;
using NUnit.Framework;
using System.Linq;
using VehicleTax.Domain;
using VehicleTax.Services;
using VehicleTax.ViewModels;

namespace VehicleTax.Test.ViewModels
{
    class ListBasicViewModelTest
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
        public void ListBasic_NullStartingEndinCategory_ValidationSuccess()
        {

            ListBasicViewModel viewModel = new ListBasicViewModel(
                endingBefore: null,
                startingAfter: null,
                limit: 10);

            Assert.IsTrue(viewModel.Result.IsValid);

        }

        [Test]
        public void ListBasic_EndingSpecifiedStartingAfterSpecified_ValidationError()
        {

            ListBasicViewModel viewModel = new ListBasicViewModel(
                endingBefore: 1110,
                startingAfter: 1112,
                limit: 10);

            Assert.IsFalse(viewModel.Result.IsValid);
            Assert.That(viewModel.Result.Errors.FirstOrDefault().ErrorMessage.Equals("'StartingAfter' cannot be specified when 'EndingBefore' is specified"));

        }

        [TestCase(100)]
        [TestCase(51)]
        [TestCase(9111)]
        public void ListBasic_LimitExceedsTreshod_ValidationError(int value)
        {

            ListBasicViewModel viewModel = new ListBasicViewModel(
                endingBefore: null,
                startingAfter: null,
                limit: value);

            Assert.IsFalse(viewModel.Result.IsValid);

        }

        [Test]
        public void ListBasic_EndingSpecifiedStartingAfterNotSpecified_ValidationSuccess()
        {

            ListBasicViewModel viewModel = new ListBasicViewModel(
                endingBefore: 1110,
                startingAfter: null,
                limit: 10);

            Assert.IsTrue(viewModel.Result.IsValid);

        }

        [Test]
        public void ListBasic_EndingNotSpecifiedStartingAfterSpecified_ValidationSuccess()
        {

            ListBasicViewModel viewModel = new ListBasicViewModel(
                endingBefore: null,
                startingAfter: 1110,
                limit: 10);

            Assert.IsTrue(viewModel.Result.IsValid);

        }

    }
}
