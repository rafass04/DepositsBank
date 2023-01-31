using BankDeposits.src.Domain;

namespace BankDeposits.src.Services
{
    internal class SendDepositEmailService : ISendDepositEmailService
    {
        public void SendDepositEmail(Deposit deposit)
        {
            //chamada para o motor de comunicação
            Console.WriteLine("E-mail de depósito efetuado enviado.");
        }
    }
}