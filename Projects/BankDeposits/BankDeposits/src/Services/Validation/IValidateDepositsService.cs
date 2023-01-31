using BankDeposits.src.Domain;

namespace BankDeposits.src.Services
{
    public interface IValidateDepositsService
    {
        Task ValidateDepositsAsync(List<Deposit> deposits);
    }
}