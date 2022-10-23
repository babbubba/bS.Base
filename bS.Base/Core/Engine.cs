using bs.Data.Interfaces;
using bS.Base.Interfaces.Core;
using bS.Base.Interfaces.Services;
using bS.Base.Interfaces.ViewModels.Responses;
using bS.Base.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace bS.Base.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="bS.Base.Interfaces.Core.IEngine" />
    public class Engine : IEngine
    {
        /// <summary>
        /// Dinamicallies the load DLLS.
        /// </summary>
        /// <param name="rootFolder">The root folder.</param>
        /// <param name="dllFileNamePattern">The DLL file name pattern.</param>
        /// <returns></returns>
        public Dictionary<string, IResponse> DinamicallyLoadDlls(string rootFolder, string dllFileNamePattern = "*.dll")
        {
            var result = new Dictionary<string, IResponse>();
            var dllPaths = Directory.GetFiles(rootFolder, dllFileNamePattern, SearchOption.AllDirectories);
            foreach (var dllPath in dllPaths)
            {
                try
                {
                    Assembly.LoadFrom(dllPath);
                    result.Add(dllPath, new Response());
                }
                catch (Exception ex)
                {
                    result.Add(dllPath, new Response { Success = false, ErrorMessage = ex.Message });
                }
            }

            return result;
        }

        /// <summary>
        /// Initializes the services.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        public Dictionary<string, IResponse> InitServices(IServiceProvider serviceProvider, string typeNameStartsWith)
        {
            var result = new Dictionary<string, IResponse>();
            var initializableServiceInterface = typeof(IInitializableService);
            IEnumerable<Type>? initializableServices = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(typeNameStartsWith))
                .SelectMany(s => s.GetTypes())
                .Where(p => initializableServiceInterface.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var initializableService in initializableServices)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var i = initializableService.GetInterfaces().Single(i => i.FullName != typeof(IBaseService).FullName && i.FullName != typeof(IInitializableService).FullName);
                    var serviceToInit = scope.ServiceProvider.GetService(i);
                    if (serviceToInit != null)
                    {
                        result.Add(i.Name, ((IInitializableService)serviceToInit).InitServiceAsync().Result);
                    }
                    else
                    {
                        result.Add(i.Name, new Response<bool> { Success = false, ErrorMessage = "Cannot resolve instance from DI Container." });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        public Dictionary<string, IResponse> RegisterRepositories(IServiceCollection serviceCollection, string typeNameStartsWith)
        {
            var result = new Dictionary<string, IResponse>();
            var repositoryBaseInterface = typeof(IRepository);

            IEnumerable<Type>? repositories = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(typeNameStartsWith))
                .SelectMany(s => s.GetTypes())
                .Where(p => repositoryBaseInterface.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var repository in repositories)
            {
                try
                {
                    var i = repository.GetInterfaces().Single(i => i.FullName != typeof(IRepository).FullName);
                    serviceCollection.AddScoped(i, repository);

                    result.Add(repository.FullName.ToString(), new Response());
                }
                catch (Exception ex)
                {
                    result.Add(repository.FullName.ToString(), new Response { Success = false, ErrorMessage = ex.Message });
                }
            }

            return result;
        }

        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        /// <param name="typeNameStartsWith">The type name starts with.</param>
        /// <returns></returns>
        public Dictionary<string, IResponse> RegisterServices(IServiceCollection serviceCollection, string typeNameStartsWith)
        {
            var result = new Dictionary<string, IResponse>();
            var serviceBaseInterface = typeof(IBaseService);

            var services = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith(typeNameStartsWith))
                .SelectMany(s => s.GetTypes())
                .Where(p => serviceBaseInterface.IsAssignableFrom(p) && !p.IsAbstract);

            foreach (var service in services)
            {
                try
                {
                    var i = service.GetInterfaces().Single(i => i.FullName != typeof(IBaseService).FullName && i.FullName != typeof(IInitializableService).FullName);
                    serviceCollection.AddScoped(i, service);
                    result.Add(service.FullName.ToString(), new Response());
                }
                catch (Exception ex)
                {
                    result.Add(service.FullName.ToString(), new Response { Success = false, ErrorMessage = ex.Message });
                }
            }

            return result;
        }
    }
}