using Microsoft.EntityFrameworkCore;
using VerifyDeposits.Models;

namespace VerifyDeposits.Repositories.DepositStatusRepository
{
    public class DepositStatusRepository : IDepositStatusRepository
    {
        private readonly DepositStatusDbContext _depositStatusDbContext;

        public DepositStatusRepository(DepositStatusDbContext depositStatusDbContext)
        {
            _depositStatusDbContext = depositStatusDbContext ?? throw new ArgumentNullException(nameof(depositStatusDbContext));
        }

        public List<DepositStatus> GetAllDepositsStatusAsync()
        {
            return _depositStatusDbContext.DepositStatus.ToList();
        }

        public async Task<DepositStatus?> GetDepositStatusByAccountAsync(string contaDestino)
        {
            var depositStatusList = await _depositStatusDbContext.DepositStatus.ToListAsync();
            return depositStatusList.FirstOrDefault(x => x.ContaDestino == contaDestino);
        }
    }
}