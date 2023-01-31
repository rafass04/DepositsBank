using BankDeposits.src.Domain;

namespace BankDeposits.src.Services.Query
{
    public interface IGetBankDepositsQuery
    {
        void SaveDepositsAsync(DepositStatus depositStatus);

        void InitializeSql();
    }
}