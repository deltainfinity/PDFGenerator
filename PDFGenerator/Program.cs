﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;

namespace PDFGenerator
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Working directory the application launched from
        /// </summary>
        public static string WorkingDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

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
        /// Create Swashbuckle API Info for API Explorer Description
        /// </summary>
        /// <param name="description">The API Explorer Description for the API</param>
        /// <returns></returns>
        public static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"PDFGenerator {description.ApiVersion}",
                Version = description.ApiVersion.ToString(),
                Description = "Vant4gePoint PDFGenerator microservice documentation.",
                Contact = new Contact() { Name = "Vant4ge Engineering", Email = "dev@vant4ge.com" },
                TermsOfService = "Proprietary",
                License = new License() { Name = "MIT", Url = "https://opensource.org/licenses/MIT" }
            };

            if (description.IsDeprecated)
            {
                info.Description += Environment.NewLine + "*** This API version has been deprecated ***";
            }

            return info;
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
