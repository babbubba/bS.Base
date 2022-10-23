using bS.Base.Interfaces.ViewModels.Responses;

namespace bS.Base.ViewModels
{
    /// <summary>
    /// The response for paged data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="bS.Base.ViewModels.Response" />
    /// <seealso cref="bS.Base.Interfaces.ViewModels.Responses.IPagedResponse&lt;T&gt;" />
    public class PagedResponse<T> : Response, IPagedResponse<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<T>? Data { get; set; }

        /// <summary>
        /// Gets or sets the draw.
        /// </summary>
        /// <value>
        /// The draw.
        /// </value>
        public int Draw { get; set; }

        /// <summary>
        /// Gets or sets the records filtered.
        /// </summary>
        /// <value>
        /// The records filtered.
        /// </value>
        public int RecordsFiltered { get; set; }

        /// <summary>
        /// Gets or sets the records total.
        /// </summary>
        /// <value>
        /// The records total.
        /// </value>
        public int RecordsTotal { get; set; }
    }
}