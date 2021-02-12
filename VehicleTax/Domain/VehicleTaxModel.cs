namespace VehicleTax.Domain
{
    public class VehicleTaxModel
    {
        public long Id { get; set; }
        public long VehicleTypeId { get; set; }
        public double ImportDuty { get; set; }
        public double Vat { get; set; }
        public double Nhil { get; set; }
        public double GetFundLevy { get; set; }
        public double AuLevy { get; set; }
        public double EcowasLevy { get; set; }
        public double EximLevy { get; set; }
        public double ExamLevy { get; set; }
        public double ProcessingFee { get; set; }
        public double SpecialImportLevy { get; set; }

        #region pagination

        internal long? Prev { get; set; }
        internal long? PreviousCursor => Prev != null ? Id : (long?)null;

        internal long? Next { get; set; }
        internal long? NextCursor => Next != null ? Id : (long?)null;

        #endregion pagination

    }
}
