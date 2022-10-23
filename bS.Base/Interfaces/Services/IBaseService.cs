using bS.Base.Interfaces.ViewModels.Responses;

namespace bS.Base.Interfaces.Services
{

    /// <summary>
    /// The base interface for all service class
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        /// Executes the function asynchronous. It auto handle exceptions and format response object with a generic value
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="genericErrorMessage">The generic error message.</param>
        /// <returns></returns>
        Task<IResponse<TResponse>> ExecuteAsync<TResponse>(Func<IResponse<TResponse>, Task<IResponse<TResponse>>> function, string genericErrorMessage);

        /// <summary>
        /// Executes the function asynchronous. It auto handle exceptions and format response (no value response)
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="genericErrorMessage">The generic error message.</param>
        /// <returns></returns>
        Task<IResponse> ExecuteAsync(Func<IResponse, Task<IResponse>> function, string genericErrorMessage);
        Task<IPagedResponse<TResponse>> ExecuteDatatableAsync<TResponse>(Func<IPagedResponse<TResponse>, Task<IPagedResponse<TResponse>>> function, string genericErrorMessage);

        /// <summary>Hashes the password.</summary>
        /// <param name="clearPassword">The clear password.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        string HashPassword(string clearPassword);

        /// <summary>
        /// Translate the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        string T(string text);

        /// <summary>
        /// Translate the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="objs">The variables for the string format.</param>
        /// <returns></returns>
        string T(string text, params object[] objs);
    }
}