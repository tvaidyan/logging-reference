using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LoggingReference.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;

        public Worker(ILogger<Worker> logger)
        {
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var testInput = "Hello from Worker process";

            logger.LogTrace($"Testing trace log entry: {testInput}");
            logger.LogDebug($"Testing debug log entry: {testInput}");
            logger.LogInformation($"Testing information log entry: {testInput}");
            logger.LogWarning($"Testing warning log entry: {testInput}");
            logger.LogError($"Testing error log entry: {testInput}");
            logger.LogCritical($"Testing critical log entry: {testInput}");

            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("HelloWorld Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
