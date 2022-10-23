namespace bS.Base.Interfaces.ViewModels.Responses
{
    /// <summary>
    /// The response for paged data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="bS.Base.Interfaces.ViewModels.Responses.IResponse" />
    public interface IPagedResponse<T> : IResponse
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        IEnumerable<T>? Data { get; set; }

        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        int Draw { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        int RecordsTotal { get; set; }
    }
}