using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace whoami.Services
{
    public class BackgroundLogService : IHostedService
    {
        readonly ILogger _logger;
        public BackgroundLogService(ILogger<BackgroundLogService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException($"{nameof(BackgroundLogService)}.ctor : {nameof(logger)} is required");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Observable.Timer(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2)).Subscribe((l) =>
            {
                _logger.LogWarning("hostname:" + Dns.GetHostName());
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
