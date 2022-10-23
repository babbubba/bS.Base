using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bS.Base.Interfaces.ViewModels.Responses
{

    /// <summary>
    /// Action or method response with generic value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="bS.Base.Interfaces.ViewModels.Responses.IResponse" />
    public interface IResponse<T> : IResponse

    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        T? Value { get; set; }
    }

    /// <summary>
    ///  Action or method response without value
    /// </summary>
    /// <seealso cref="italcom.TodoSales.Infrastructure.ViewModels.Responses.IResponseViewModel" />
    public interface IResponse
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        string? ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IResponse{T}"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        bool Success { get; set; }

        /// <summary>
        /// Gets or sets the warn message.
        /// </summary>
        /// <value>
        /// The warn message.
        /// </value>
        string? WarnMessage { get; set; }
    }
}
