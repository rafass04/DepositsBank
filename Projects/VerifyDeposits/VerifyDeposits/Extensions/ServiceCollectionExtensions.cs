using Microsoft.OpenApi.Models;
using System.Data.SQLite;
using VerifyDeposits.Models;
using VerifyDeposits.Repositories.DepositStatusRepository;

namespace VerifyDeposits.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwagger();
            return builder;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { });
            });
            return services;
        }

        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            string directory = @"..\..\Database";
            string relativePath = string.Concat(directory, @"\Database.sqlite");
            string connectionString = string.Format("Data Source={0}", relativePath);

            builder.Services.AddSqlite<DepositStatusDbContext>(connectionString);

            builder.Services.AddScoped(_ => new SQLiteConnection(connectionString));

            return builder;
        }

        public static WebApplicationBuilder AddRepositories(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDepositStatusRepository, DepositStatusRepository>();
            return builder;
        }
    }
}