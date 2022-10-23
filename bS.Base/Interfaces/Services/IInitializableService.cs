using bS.Base.Interfaces.ViewModels.Responses;

namespace bS.Base.Interfaces.Services
{
    /// <summary>
    /// The class that implements this interface will be intializaed at bootstrap
    /// </summary>
    public interface IInitializableService
    {
        /// <summary>
        /// Initializes the service asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<IResponse> InitServiceAsync();
    }
}