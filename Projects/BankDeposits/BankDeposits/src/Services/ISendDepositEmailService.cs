using BankDeposits.src.Domain;

namespace BankDeposits.src.Services
{
    public interface ISendDepositEmailService
    {
        void SendDepositEmail(Deposit deposit);
    }
}