using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Simulador.Backend.Shared;

namespace Simulador.Backend.Application.Common
{
    public class BaseApp<T>
    {

        public readonly ILogger _logger;

        public BaseApp(ILogger logger)
        {
            _logger = logger;
        }

        protected async Task<StatusSimpleResponse> ProcesoSimple(Func<Task> callback, string titulo)
        {
            var response = new StatusSimpleResponse();

            try
            {
                await callback();

                response.Success = true;
                response.Title = titulo;
            }
            catch (CustomException customEx)
            {
                _logger.LogError(
                    customEx,
                    "Version: {0}, ExId: {1}, Metodo: {2}, TargetType: {3}",
                    MaestraConstante.APP_VERSION,
                    response.Id,
                    callback.Method.DeclaringType.FullName,
                    callback.Target?.GetType().FullName ?? "null"
                );
                response.Code = customEx.Codigo;
                response.Success = false;
                response.Title = customEx.Titulo;
                response.Detail = customEx.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Version: {0}, ExId: {1}, Metodo: {2}, TargetType: {3}",
                    MaestraConstante.APP_VERSION,
                    response.Id,
                    callback.Method.DeclaringType.FullName,
                    callback.Target?.GetType().FullName ?? "null"
                );
                response.Success = false;
                response.Title = "Sucedió un error inesperado.";
                response.Detail = ex.ToString();
            }

            return response;
        }


        protected async Task<StatusResponse<T>> ProcesoComplejo<T>(Func<Task<T>> callbackData, string titulo = "")
        {
            var response = new StatusResponse<T>();

            try
            {
                response.Data = await callbackData();

                response.Title = titulo;
                response.Success = true;
            }
            catch (CustomException customEx)
            {
                _logger.LogError(
                    customEx,
                    "Version: {0}, ExId: {1}, Metodo: {2}, TargetType: {3}",
                    MaestraConstante.APP_VERSION,
                    response.Id,
                    callbackData.Method.DeclaringType.FullName,
                    callbackData.Target?.GetType().FullName ?? "null"
                );
                response.Code = customEx.Codigo;
                response.Title = customEx.Titulo;
                response.Detail = customEx.ToString();
                response.Success = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Version: {0}, ExId: {1}, Metodo: {2}, TargetType: {3}",
                    MaestraConstante.APP_VERSION,
                    response.Id,
                    callbackData.Method.DeclaringType.FullName,
                    callbackData.Target?.GetType().FullName ?? "null"
                );

                response.Title = "Sucedió un error inesperado.";
                response.Detail = ex.ToString();
                response.Success = false;
            }

            return response;
        }


        public Dictionary<string, List<string>> GetErrors(List<ValidationFailure> errors)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            foreach (ValidationFailure failure in errors)
            {
                List<string> errorsList = null;
                if (!result.TryGetValue(failure.PropertyName, out errorsList))
                {
                    errorsList = new List<string>();
                    result[failure.PropertyName] = errorsList;
                }

                errorsList.Add(failure.ErrorMessage);
            }
            return result;
        }
    }
}
