namespace VehicleTax.Response
{
    public class ListResponse<T>
    {
        /// <summary>
        /// String representing the object’s type. Objects of the same type share the same value.
        /// Value is `list`
        /// </summary>
        public string Object => "list";

        /// <summary>
        /// An list containing the actual objects.
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// Specifies the object id to specify as `StartingAfter` when fetching the next page.
        /// Null if there are no more objects to retrieve.
        /// </summary>
        public long? NextCursor { get; set; }

        /// <summary>
        /// Specifies the object id to specify as `EndingBefore` when fetching the previous page.
        /// Null when this is the last page of objects.
        /// </summary>
        public long? PreviousCursor { get; set; }

        /// <summary>
        /// Specifies the toatl number of objects retrived
        /// Null when not specified
        /// </summary>
        public int? TotalRecords { get; internal set; }

    }
}
