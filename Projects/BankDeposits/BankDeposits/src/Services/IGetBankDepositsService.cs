using BankDeposits.src.Domain;

namespace BankDeposits.src.Services
{
    public interface IGetBankDepositsService
    {
        Task<List<Deposit>> GetBankDepositsAsync();
    }
}