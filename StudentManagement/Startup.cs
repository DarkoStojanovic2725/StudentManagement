using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using App.Metrics;
using App.Metrics.Extensions.Reporting.InfluxDB;
using App.Metrics.Extensions.Reporting.InfluxDB.Client;
using App.Metrics.Extensions.Reporting.TextFile;
using App.Metrics.Reporting.Abstractions;
using App.Metrics.Reporting.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Owin;
using Serilog;
using StudentManagement.Infrastructure;

[assembly: OwinStartupAttribute(typeof(StudentManagement.Startup))]
namespace StudentManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.SetDependencyResolver();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.RollingFile(Path.Combine($@"C:\logs\{Assembly.GetExecutingAssembly().GetName().Name}", "log-{Date}.txt"))
                .CreateLogger();

            app.UseMetrics(provider);

            var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

            loggerFactory.AddSerilog(Log.Logger);

            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            {
                var reportFactory = provider.GetRequiredService<IReportFactory>();
                var metrics = provider.GetRequiredService<IMetrics>();
                var reporter = reportFactory.CreateReporter();
                reporter.RunReports(metrics, cancellationToken);
            });

            ConfigureAuth(app);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddControllersAsServices();

            services
                .AddMetrics(options =>
                {
                    options.DefaultContextLabel = "StudentManagement";
                    options.ReportingEnabled = true;
                }, Assembly.GetExecutingAssembly().GetName())
                .AddReporting(factory =>
                {
                    factory.AddInfluxDb(new InfluxDBReporterSettings
                    {
                        HttpPolicy = new HttpPolicy
                        {
                            FailuresBeforeBackoff = 3,
                            BackoffPeriod = TimeSpan.FromSeconds(30),
                            Timeout = TimeSpan.FromSeconds(3)
                        },
                        InfluxDbSettings = new InfluxDBSettings("StudentManagementMetrics", new Uri("http://127.0.0.1:8086"))
                        {
                            
                        },
                        ReportInterval = TimeSpan.FromSeconds(5)
                    });

                    factory.AddTextFile(new TextFileReporterSettings
                    {
                        ReportInterval = TimeSpan.FromSeconds(30),
                        FileName = @"C:\metrics\StudentManagementMetrics.txt"
                    });

                })
                .AddHealthChecks(factory =>
                {
                    factory.RegisterProcessPrivateMemorySizeHealthCheck("Private Memory Size", 200);
                    factory.RegisterProcessVirtualMemorySizeHealthCheck("Virtual Memory Size", 200);
                    factory.RegisterProcessPhysicalMemoryHealthCheck("Working Set", 200);
                })
                .AddJsonSerialization()
                .AddMetricsMiddleware(options =>
                {

                });
        }
    }
}
