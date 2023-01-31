using BankDeposits.src.Domain;
using BankDeposits.src.Domain.Enum;
using BankDeposits.src.Services.Query;
using System.Text.Json;

namespace BankDeposits.src.Services
{
    public class ValidateDepositsService : IValidateDepositsService
    {
        private readonly IGetBankDepositsQuery _getBankDepositsQuery;
        private readonly ISendDepositEmailService _sendDepositEmailService;

        public ValidateDepositsService(IGetBankDepositsQuery getBankDepositsQuery, ISendDepositEmailService sendDepositEmailService)
        {
            _getBankDepositsQuery = getBankDepositsQuery ?? throw new ArgumentNullException(nameof(getBankDepositsQuery));
            _sendDepositEmailService = sendDepositEmailService ?? throw new ArgumentNullException(nameof(sendDepositEmailService));
        }

        public async Task ValidateDepositsAsync(List<Deposit> deposits)
        {
            using HttpClient client = new();

            try
            {
                var json = await client.GetStringAsync(
                    "https://run.mocky.io/v3/7f0acd4b-e63d-4571-b834-c3db15f70673");

                var customersBankInformation = JsonSerializer.Deserialize<List<CustomerBankInformation>>(json);

                if (customersBankInformation == null || !customersBankInformation.Any())
                {
                    Console.WriteLine("Nenhuma conta com essa agência foi encontrada no banco Novidade.");
                    return;
                }

                foreach (var deposit in deposits)
                {
                    var customerBankInformation = customersBankInformation
                        .Find(x => x.Id == deposit.Id);

                    var depositStatus = new DepositStatus(deposit);

                    if (customerBankInformation == null)
                    {
                        Console.WriteLine($"O depósito para conta destino {deposit.ContaDestino} não foi efetuado.");
                        depositStatus.Status = Status.Erro;
                        depositStatus.Razao = "Dados do cliente não encontrados no banco Novidade.";
                        _getBankDepositsQuery.SaveDepositsAsync(depositStatus);

                        continue;
                    }

                    if (deposit.AgenciaDestino != customerBankInformation?.Agencia)
                    {
                        Console.WriteLine("O depósito não foi efetuado, a agência de destino não corresponde.");

                        depositStatus.Status = Status.Erro;
                        depositStatus.Razao = "A agência de destino de depósito não corresponde a agência de destino do cliente.";
                        _getBankDepositsQuery.SaveDepositsAsync(depositStatus);

                        continue;
                    }

                    if (deposit.ContaDestino != customerBankInformation?.Conta)
                    {
                        Console.WriteLine("O depósito não foi efetuado, a conta de destino de destino não corresponde.");

                        depositStatus.Status = Status.Erro;
                        depositStatus.Razao = "A conta de destino de depósito não corresponde a conta de destino do cliente.";
                        _getBankDepositsQuery.SaveDepositsAsync(depositStatus);

                        continue;
                    }

                    if (deposit.Nome != customerBankInformation?.Nome)
                    {
                        Console.WriteLine("O depósito não foi efetuado, o nome de destino não corresponde.");

                        depositStatus.Status = Status.Erro;
                        depositStatus.Razao = "O nome do(a) cliente de depósito não corresponde ao nome do(a) cliente.";
                        _getBankDepositsQuery.SaveDepositsAsync(depositStatus);

                        continue;
                    }

                    Console.WriteLine($"O depósito para conta destino {deposit.ContaDestino} foi efetuado com sucesso.");
                    depositStatus.Status = Status.Successo;
                    _sendDepositEmailService.SendDepositEmail(deposit);
                    _getBankDepositsQuery.SaveDepositsAsync(depositStatus);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao validar depósitos. {ex.Message}");
            }
        }
    }
}