using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Azure.Core;
using Microsoft.Extensions.Configuration;



namespace DataAccess.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString , string environmentName, IConfiguration config)
        {
            TokenCredential credential;

            if (environmentName == "Development")
            {
                credential = new AzureCliCredential();
            }
            else
            {
                var clientId = config["ManagedIdentityClientId"];
                credential = new ManagedIdentityCredential(clientId);
            }

        services.AddDbContext<AppDbContext>((provider, options) =>
            {
                // Use SqlConnection to set AccessToken
                var builder = new SqlConnectionStringBuilder(connectionString);
                var conn = new SqlConnection(builder.ConnectionString);

                options.UseSqlServer(conn, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });

                options.AddInterceptors(new AzureSqlAuthenticationInterceptor(credential));
            });


            // Register Services
            services.AddScoped<ILogInfoService, LogInfoService>();
            services.AddScoped<IClickInfoService, ClickInfoService>();

            return services;
        }


    }
}
