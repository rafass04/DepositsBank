using VerifyDeposits.Models;

namespace VerifyDeposits.Repositories.DepositStatusRepository
{
    public interface IDepositStatusRepository
    {
        List<DepositStatus> GetAllDepositsStatusAsync();

        Task<DepositStatus?> GetDepositStatusByAccountAsync(string contaDestino);
    }
}