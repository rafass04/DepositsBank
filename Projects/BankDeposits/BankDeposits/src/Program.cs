using BankDeposits.src.Services;
using BankDeposits.src.Services.DatabaseContext;
using BankDeposits.src.Services.Query;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SQLite;

namespace BankDeposits.src
{
    public class Program
    {
        private static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var getBankDepositsService = serviceProvider.GetService<IGetBankDepositsService>();
            var validateDepositsService = serviceProvider.GetService<IValidateDepositsService>();
            var getBankDepositsQuery = serviceProvider.GetService<IGetBankDepositsQuery>();

            getBankDepositsQuery?.InitializeSql();

            var deposits = await getBankDepositsService!.GetBankDepositsAsync();

            await validateDepositsService!.ValidateDepositsAsync(deposits);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGetBankDepositsService, GetBankDepositsService>();
            services.AddScoped<IValidateDepositsService, ValidateDepositsService>();
            services.AddScoped<IGetBankDepositsQuery, GetBankDepositsQuery>();
            services.AddScoped<ISendDepositEmailService, SendDepositEmailService>();

            string directory = @"..\..\..\..\..\Database";
            string relativePath = string.Concat(directory, @"\Database.sqlite");
            string connectionString = string.Format("Data Source={0}", relativePath);
            SQLiteConnection.CreateFile(relativePath);

            services.AddSqlite<DepositStatusDbContext>(connectionString);
            services.AddScoped(_ => new SQLiteConnection(connectionString));
        }
    }
}