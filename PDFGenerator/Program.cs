using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PDFGenerator
{
    public class Program
    {
        /// <summary>
        /// .NET Configuration Service
        /// </summary>
        public static IConfiguration Configuration => new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appSettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Web host builder for creating the kestrel instance
        /// </summary>
        /// <param name="args">Arguments to pass in to the default builder</param>
        /// <returns>The builder instance or null if something blew up</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseIISIntegration()
            .UseConfiguration(Configuration)
            .UseStartup<Startup>();
    }
}
