using BankDeposits.src.Domain;
using System.Text.Json;

namespace BankDeposits.src.Services
{
    public class GetBankDepositsService : IGetBankDepositsService
    {
        public async Task<List<Deposit>> GetBankDepositsAsync()
        {
            using HttpClient client = new();

            try
            {
                var json = await client.GetStringAsync(
                    "https://run.mocky.io/v3/68cc9f8b-519b-4057-bf3c-804115e68fd4");

                var deposits = JsonSerializer.Deserialize<List<Deposit>>(json);

                return deposits!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro enquanto os dados de depósitos eram recebidos. Mensagem: {ex.Message}");
                return null!;
            }
        }
    }
}