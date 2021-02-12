namespace VehicleTax.Domain
{
    public class VehicleTaxDto
    {
        public long Id { get; set; }
        public long VehicleTypeId { get; set; }
        public string VehicleTypeName { get; set; }
        public string VehicleTypeDescription { get; set; }
        public long VehicleCategoryId { get; set; }
        public string VehicleCategoryName { get; set; }
        public string VehicleCategoryDescription { get; set; }
        public double ImportDuty { get; set; }
        public double Vat { get; set; }
        public double Nhil { get; set; }
        public double GetfundLevy { get; set; }
        public double AuLevy { get; set; }
        public double EcowasLevy { get; set; }
        public double EximLevy { get; set; }
        public double ExamLevy { get; set; }
        public double ProcessingFee { get; set; }
        public double SpecialImportLevy { get; set; }


    }
}
