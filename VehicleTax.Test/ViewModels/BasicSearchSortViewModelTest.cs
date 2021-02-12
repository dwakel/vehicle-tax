using FluentResults;
using NUnit.Framework;
using System.Linq;
using VehicleTax.Domain;
using VehicleTax.Services;
using VehicleTax.ViewModels;

namespace VehicleTax.Test.ViewModels
{
    class BasicSearchSortViewModelTest
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

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(20)]
        public void BasicSearch_PageAndPerPAge_ValidationSuccess(int value)
        {

            BasicSearchSortViewModel viewModel = new BasicSearchSortViewModel(
                searchBy: null,
                sortBy: null,
                page: value,
                perPage: value);

            Assert.IsTrue(viewModel.Result.IsValid);

        }

        [TestCase(1001)]
        [TestCase(9999)]
        [TestCase(20000)]
        public void BasicSearch_PerPageExceedLimit_ValidationError(int value)
        {

            BasicSearchSortViewModel viewModel = new BasicSearchSortViewModel(
                searchBy: null,
                sortBy: null,
                page: 1,
                perPage: value);

            Assert.IsFalse(viewModel.Result.IsValid);

        }

        

    }
}
