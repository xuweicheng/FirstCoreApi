using Microsoft.Extensions.Logging;
using System;

namespace FirstCoreApi.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailToAddress = Startup.Config["mailSettings:mailToAddress"];
        private string _mailFromAddress = Startup.Config["mailSettings:mailFromAddress"];
        private ILogger<LocalMailService> _logger;

        public LocalMailService(ILogger<LocalMailService> logger)
        {
            _logger = logger;
        }

        public void Send(string subject, string message)
        {
            _logger.LogInformation($"LocalMailService From {_mailFromAddress} To {_mailToAddress}");

        }
    }
}
