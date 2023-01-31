using BankDeposits.src.Domain.Enum;

namespace BankDeposits.src.Domain
{
    public class DepositStatus
    {
        public DepositStatus(Deposit? deposit)
        {
            Deposit = deposit;
        }

        public Deposit? Deposit { get; set; }

        public Status? Status { get; set; }

        public string? Razao { get; set; }
    }
}