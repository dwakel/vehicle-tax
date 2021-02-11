namespace VehicleTax.Domain
{
    public class VehicleTypeModel
    {
        public long Id { get; set; }
        public long VehicleCategoryId { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        #region pagination

        internal long? Prev { get; set; }
        internal long? PreviousCursor => Prev != null ? Id : (long?)null;

        internal long? Next { get; set; }
        internal long? NextCursor => Next != null ? Id : (long?)null;

        #endregion pagination

    }
}
