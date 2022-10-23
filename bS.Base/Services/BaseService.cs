using AutoMapper;
using bs.Data.Interfaces;
using bS.Base.Interfaces.Services;
using bS.Base.Interfaces.ViewModels.Responses;
using bS.Base.ViewModels;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace bS.Base.Services
{
    public abstract class BaseService : IBaseService
    {
        /// <summary>
        /// The log
        /// </summary>
        protected readonly ILogger log;

        /// <summary>
        /// The mapper
        /// </summary>
        protected readonly IMapper mapper;

        /// <summary>
        /// The unit of work
        /// </summary>
        protected readonly IUnitOfWork unitOfWork;

        private readonly IDatatableService datatableService;
        private readonly ITranslateService translate;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="translateService">The translate service.</param>
        /// <param name="datatableService">The datatable service.</param>
        /// <param name="log">The log.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public BaseService(IMapper mapper, ITranslateService translateService, IDatatableService datatableService, ILogger log, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.translate = translateService;
            this.datatableService = datatableService;
            this.log = log;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Executes the function asynchronous. It auto handle exceptions and format response object with a generic value
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="genericErrorMessage">The generic error message.</param>
        /// <returns></returns>
        public async Task<IResponse<TResponse>> ExecuteAsync<TResponse>(Func<IResponse<TResponse>, Task<IResponse<TResponse>>> function, string genericErrorMessage)
        {
            if (Activator.CreateInstance(typeof(Response<TResponse>)) is not IResponse<TResponse> response) throw new ApplicationException(T("Eccezione nella generazione del tipo di risposta"));

            if (function == null)
            {
                response.Success = false;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + translate.Translate("Errore eseguendo l'azione. L'azione è nulla!");
                return response;
            }

            try
            {
                // try executing the operation method
                response = await function.Invoke(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var error = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                response.Success = false;
                log.LogError(ex, error);
            }

            // return the response
            return response;
        }

        /// <summary>
        /// Executes the function asynchronous. It auto handle exceptions and format response (no value response)
        /// </summary>
        /// <param name="function">The function.</param>
        /// <param name="genericErrorMessage">The generic error message.</param>
        /// <returns></returns>
        public async Task<IResponse> ExecuteAsync(Func<IResponse, Task<IResponse>> function, string genericErrorMessage)
        {
            if (Activator.CreateInstance(typeof(Response)) is not IResponse response) throw new ApplicationException(T("Eccezione nella generazione del tipo di risposta"));

            if (function == null)
            {
                response.Success = false;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + translate.Translate("Errore eseguendo l'azione. L'azione è nulla!");
                return response;
            }

            try
            {
                // try executing the operation method
                response = await function.Invoke(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var error = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                response.Success = false;
                log.LogError(ex, error);
            }

            // return the response
            return response;
        }

        /// <summary>
        /// Executes the function asynchronous. It auto handle exceptions and format response object with a generic value
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="genericErrorMessage">The generic error message.</param>
        /// <returns></returns>
        public async Task<IPagedResponse<TResponse>> ExecuteDatatableAsync<TResponse>(Func<IPagedResponse<TResponse>, Task<IPagedResponse<TResponse>>> function, string genericErrorMessage)
        {
            if (Activator.CreateInstance(typeof(PagedResponse<TResponse>)) is not IPagedResponse<TResponse> response) throw new ApplicationException(T("Eccezione nella generazione del tipo di risposta"));

            if (function == null)
            {
                response.Success = false;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + T("Errore eseguendo l'azione. L'azione è nulla!");
                return response;
            }

            try
            {
                // try executing the operation method
                response = await function.Invoke(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                response.Success = false;
                var error = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                response.ErrorMessage = genericErrorMessage + System.Environment.NewLine + ex.GetBaseException().Message;
                log.LogError(ex, error);
            }

            // return the response
            return response;
        }

        public string HashPassword(string clearPassword)
        {
            //byte[] salt;
            //new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var pbkdf2 = new Rfc2898DeriveBytes(clearPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }

        /// <summary>
        /// Translate the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public string T(string text)
        {
            return translate.Translate(text);
        }

        /// <summary>
        /// Translate the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="objs">The objs.</param>
        /// <returns></returns>
        public string T(string text, params object[] objs)
        {
            return translate.Translate(text, objs);
        }
    }
}