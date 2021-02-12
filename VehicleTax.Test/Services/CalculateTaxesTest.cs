using NUnit.Framework;
using VehicleTax.Domain;
using VehicleTax.Services;

namespace VehicleTax.Test.Services
{
    class CalculateTaxesTest
    {
        private VehicleTaxDto _vehicleTax;
        private readonly double CIF = 100000;

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
        public void CalculateDuty_InputCIFAndVehicleTaxpercentages_ReturnCorrectDuty()
        {
            double duty = _vehicleTax.CalculateDuty(CIF: CIF);
            Assert.IsTrue(duty is 58125.00000000001);
        }

        [Test]
        public void CalculateVat_InputCIFAndVehicleTaxpercentages_ReturnCorrectDuty()
        {
            double vat = _vehicleTax.CalculateVat(CIF: CIF);
            Assert.IsTrue(vat is 15625);
        }
    }
}
