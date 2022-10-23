using bS.Base.Interfaces.ViewModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bS.Base.ViewModels
{
    public class Response : IResponse

    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        public Response()
        {
            Success = true;
        }
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:italcom.TodoSales.Infrastructure.ViewModels.Responses.IResponseViewModel`1" /> is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the warn message.
        /// </summary>
        /// <value>
        /// The warn message.
        /// </value>
        public string? WarnMessage { get; set; }
    }

    public class Response<T> : Response, IResponse<T>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public T? Value { get; set; }
    }
}
