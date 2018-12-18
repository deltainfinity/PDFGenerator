using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using PDFGenerator.Exceptions;
using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace PDFGenerator.Configuration
{
    /// <summary>
    /// Class to configure Serilog
    /// </summary>
    public class SerilogConfig
    {
        /// <summary>
        /// Configure Serilog
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        /// <param name="configuration">Configuration</param>
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var appName = configuration["ApplicationName"] as string;
            if (string.IsNullOrEmpty(appName))
            {
                throw new ConfigurationErrorsException("ApplicationName is not set in the app.settings.");
            }

            var deploymentMode = configuration["DeploymentMode"] as string;
            if (string.IsNullOrEmpty(deploymentMode))
            {
                throw new ConfigurationErrorsException("DeploymentMode is not set in the app.settings.");
            }

            var baseLogPath = configuration["Logging:Path"] as string;

            if (string.IsNullOrEmpty(baseLogPath))
            {
                throw new ConfigurationErrorsException("Logging:Path is not set in the app.settings.");
            }

            if (!CloudStorageAccount.TryParse(configuration.GetConnectionString("AzureSerilogStorage"), out CloudStorageAccount azureStorageAccount))
            {
                throw new ConfigurationErrorsException("AzureSerilogStorage Connection String is not set in the app.settings.");
            }

            var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] <" + deploymentMode + "|{SourceContext}|{CorrelationId}> {Message}{NewLine}{NewLine}{Exception}{NewLine}";

            var appLogName = $"{appName}-{deploymentMode.ToLower()}";
            var detailedLogFile = baseLogPath + "\\" + appLogName + "-{Date}.log";

            switch (deploymentMode.ToUpper())
            {
                case "LOCAL":
                    // This will only write to the local file system as this is the dev's local machine.
                    Log.Logger = new LoggerConfiguration()
                                 .Enrich.FromLogContext()
                                 .Enrich.WithMachineName()
                                 .Enrich.WithEnvironmentUserName()
                                 .MinimumLevel.Debug()
                                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                 .WriteTo.RollingFile(detailedLogFile, LogEventLevel.Debug, outputTemplate, null, 1024 * 500) // 500MB
                                 .WriteTo.AzureTableStorage(azureStorageAccount, storageTableName: appName, restrictedToMinimumLevel: LogEventLevel.Debug)
                                 .Filter.ByExcluding(Matching.FromSource(""))
                                 .CreateLogger();
                    break;

                case "DEV":
                case "QA":
                    Log.Logger = new LoggerConfiguration()
                                 .Enrich.FromLogContext()
                                 .Enrich.WithMachineName()
                                 .Enrich.WithEnvironmentUserName()
                                 .MinimumLevel.Debug()
                                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                 .WriteTo.AzureTableStorage(azureStorageAccount, storageTableName: appName, restrictedToMinimumLevel: LogEventLevel.Debug)
                                 .CreateLogger();
                    break;

                case "STAGE":
                case "TEST":
                case "DEMO":
                case "TRAINING":
                case "PROD":
                    Log.Logger = new LoggerConfiguration()
                                 .Enrich.FromLogContext()
                                 .Enrich.WithMachineName()
                                 .Enrich.WithEnvironmentUserName()
                                 .MinimumLevel.Information()
                                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                                 .WriteTo.AzureTableStorage(azureStorageAccount, storageTableName: appName, restrictedToMinimumLevel: LogEventLevel.Information)
                                 .CreateLogger();
                    break;

                default:
                    throw new IndexOutOfRangeException($"Unknown Deployment Mode found: {deploymentMode}");
            }

            serviceCollection.AddLogging(lb => lb.AddSerilog(dispose: true));

            var logger = Log.ForContext<SerilogConfig>();
            if (deploymentMode.ToUpper() == "LOCAL")
            {
                logger.Information($"Detailed log file will be written to files in {baseLogPath}");
                logger.Information($"Detailed log data is being sent to the Azure Table Storage Emulator in the table named {appName} using the account {azureStorageAccount.Credentials.AccountName}");
            }
            else
            {
                logger.Information($"Detailed log data is being sent to the Azure Table Storage in the table named {appName} using the account {azureStorageAccount.Credentials.AccountName}");
            }
            logger.Debug("Startup -> Logging Configuration: COMPLETE");
        }
    }
}
