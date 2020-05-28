using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;

namespace LoggingReference.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging((hostContext, logging) =>
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File(formatter: new JsonFormatter(), path: @"logs\hello-world-worker.log",
                    rollingInterval: RollingInterval.Day)
                    .ReadFrom.Configuration(hostContext.Configuration)
                    .CreateLogger();

                logging.ClearProviders().AddSerilog();

            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<Worker>();

                // This folder structure needs to exist when running this application in Linux containers
                // It allows for ApplicationInsights to store logs locally within the container
                // and send it to the cloud, if there are network glitches that prevent the initial attempt.
                // https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core#frequently-asked-questions
                services.AddSingleton(typeof(ITelemetryChannel),
                                    new ServerTelemetryChannel() { StorageFolder = "/tmp/myfolder" });

                services.AddApplicationInsightsTelemetryWorkerService();
            });
    }
}
