using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Simulador.Backend.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Infraestructure.NotificationEmail
{
    public class NotificationEmailApp : BaseApp<NotificationEmailApp>
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public NotificationEmailApp(
            IConfiguration config,
            ILogger logger) : base(logger)
        {
            _logger = logger;
            _config = config;
        }
    }
}
