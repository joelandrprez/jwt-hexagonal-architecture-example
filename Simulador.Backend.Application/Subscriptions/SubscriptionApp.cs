using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simulador.Backend.Application.Auth;
using Simulador.Backend.Application.Common;
using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Auth.Interfaces;
using Simulador.Backend.Domain.Subscriptions.Domain;
using Simulador.Backend.Domain.Subscriptions.Dto;
using Simulador.Backend.Domain.Subscriptions.Interfaces;
using Simulador.Backend.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Application.Subscriptions
{
    public class SubscriptionApp : BaseApp<UserApp>
    {
        private readonly ILogger _logger;
        private readonly ISubcriptionRepository _subcriptionRepository;
        private readonly IConfiguration _config;

        public SubscriptionApp(
            ILogger logger, 
            ISubcriptionRepository subcriptionRepository,
            IConfiguration config) : base(logger)
        {
            _logger = logger;
            _subcriptionRepository = subcriptionRepository;
            _config = config;
        }

        public async Task<StatusResponse<List<SubscriptionResponse>>> GetByUserId(string userId)
        {
            StatusResponse<List<SubscriptionResponse>> response = new StatusResponse<List<SubscriptionResponse>>();

            Guid? ouputUserId = null;
            if (Guid.TryParse(userId, out Guid result))
            {
                ouputUserId = result;
            }
            else
            {
                response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_DATOS_ENVIADOS;
                response.Success = false;
                response.Title = MaestraConstante.MENSAJE_OPERACION_FALLIDA;
                response.Detail = "El envío falló debido a un identificador incorrecto. Revisa los datos e inténtalo otra vez.";
                return response;
            }

            StatusResponse<List<Subscription>> userSubscription = await this.ProcesoComplejo(() => _subcriptionRepository.FindByUserId(ouputUserId.Value), "");

            if (!userSubscription.Success)
            {
                response.Code = MaestraConstante.CODIGO_RESPUESTA_ERROR_GENERAL;
                response.Id = userSubscription.Id;
                response.Success = userSubscription.Success;
                response.Title = userSubscription.Title;
                response.Detail = userSubscription.Detail;
                response.Errors = userSubscription.Errors;
                return response;
            }
            List<SubscriptionResponse> listado = new List<SubscriptionResponse>();
            if (userSubscription.Data != null)
            {
                foreach (var item in userSubscription.Data)
                {
                    SubscriptionResponse subscriptionResponse = new SubscriptionResponse();
                    subscriptionResponse = SubscriptionMapper.ToDto(item);
                    listado.Add(subscriptionResponse);
                }
            }

            response.Success = true;
            response.Title = MaestraConstante.MENSAJE_OPERACION_EXITOSA;
            response.Code = MaestraConstante.CODIGO_RESPUESTA_EXITOSA;
            response.Data = listado;

            return response;
        }
        
    }
}
