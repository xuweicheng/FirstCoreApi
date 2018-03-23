using Microsoft.Extensions.Logging;
using System;

namespace FirstCoreApi.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = Startup.Config["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Config["mailSettings:mailFromAddress"];
        private ILogger<CloudMailService> _logger;

        public CloudMailService(ILogger<CloudMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string message)
        {
            _logger.LogInformation($"CloudMailService From {_mailFrom} To {_mailTo}");

        }
    }
}
