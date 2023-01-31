using BankDeposits.src.Domain;
using Microsoft.EntityFrameworkCore;

namespace BankDeposits.src.Services.DatabaseContext
{
    public class DepositStatusDbContext : DbContext
    {
        public DepositStatusDbContext(DbContextOptions<DepositStatusDbContext> options)
            : base(options) { }

        public DbSet<DepositStatus> DepositStatus => Set<DepositStatus>();
    }
}