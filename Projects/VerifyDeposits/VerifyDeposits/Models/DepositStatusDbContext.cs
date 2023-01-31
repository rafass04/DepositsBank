using Microsoft.EntityFrameworkCore;

namespace VerifyDeposits.Models
{
    public class DepositStatusDbContext : DbContext
    {
        public DepositStatusDbContext(DbContextOptions<DepositStatusDbContext> options)
            : base(options) { }

        public DbSet<DepositStatus> DepositStatus => Set<DepositStatus>();
    }
}