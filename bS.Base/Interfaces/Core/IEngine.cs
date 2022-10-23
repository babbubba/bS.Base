using bS.Base.Interfaces.ViewModels.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace bS.Base.Interfaces.Core
{
    /// <summary>
    /// Utilities for the application bootstrap
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Dinamicallies the load DLLS.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <param name="dllFileNamePattern">The DLL file name pattern.</param>
        /// <returns></returns>
        Dictionary<string, IResponse> DinamicallyLoadDlls(string rootFolder, string dllFileNamePattern);

        /// <summary>
        /// Initializes the services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        Dictionary<string, IResponse> InitServices(IServiceProvider serviceProvider, string typeNameStartsWith);

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        Dictionary<string, IResponse> RegisterRepositories(IServiceCollection serviceCollection, string typeNameStartsWith);

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        Dictionary<string, IResponse> RegisterServices(IServiceCollection serviceCollection, string typeNameStartsWith);
    }
}