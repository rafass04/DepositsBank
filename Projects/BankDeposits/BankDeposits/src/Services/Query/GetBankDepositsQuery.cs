using BankDeposits.src.Domain;
using System.Data.SQLite;

namespace BankDeposits.src.Services.Query
{
    public class GetBankDepositsQuery : IGetBankDepositsQuery
    {
        private readonly SQLiteConnection _sqliteConnection;

        public GetBankDepositsQuery(SQLiteConnection sqliteConnection)
        {
            _sqliteConnection = sqliteConnection ?? throw new ArgumentNullException(nameof(sqliteConnection));
        }

        public void SaveDepositsAsync(DepositStatus depositStatus)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = _sqliteConnection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO DepositStatus" +
                                    "(Id, " +
                                    "Nome, " +
                                    "AgenciaDestino, " +
                                    "ContaDestino, " +
                                    "AgenciaOrigem, " +
                                    "ContaOrigem, " +
                                    "Valor, " +
                                    "DataTransacao," +
                                    "Status," +
                                    "Razao) " +
                                    "VALUES(" +
                                    "@id, " +
                                    "@nome, " +
                                    "@agenciaDestino, " +
                                    "@contaDestino, " +
                                    "@agenciaOrigem, " +
                                    "@contaOrigem, " +
                                    "@valor, " +
                                    "@dataTransacao, " +
                                    "@status, " +
                                    "@razao)";
            sqlite_cmd.Parameters.AddWithValue("@Id", depositStatus.Deposit!.Id);
            sqlite_cmd.Parameters.AddWithValue("@Nome", depositStatus.Deposit!.Nome);
            sqlite_cmd.Parameters.AddWithValue("@AgenciaDestino", depositStatus.Deposit!.AgenciaDestino);
            sqlite_cmd.Parameters.AddWithValue("@ContaDestino", depositStatus.Deposit!.ContaDestino);
            sqlite_cmd.Parameters.AddWithValue("@AgenciaOrigem", depositStatus.Deposit!.AgenciaOrigem);
            sqlite_cmd.Parameters.AddWithValue("@ContaOrigem", depositStatus.Deposit!.ContaOrigem);
            sqlite_cmd.Parameters.AddWithValue("@Valor", depositStatus.Deposit!.Valor);
            sqlite_cmd.Parameters.AddWithValue("@DataTransacao", depositStatus.Deposit!.DataTransacao);
            sqlite_cmd.Parameters.AddWithValue("@Status", depositStatus.Status.ToString());
            sqlite_cmd.Parameters.AddWithValue("@Razao", depositStatus.Razao);

            sqlite_cmd.ExecuteNonQuery();
        }

        public void InitializeSql()
        {
            try
            {
                _sqliteConnection.Open();

                SQLiteCommand sqlite_cmd;

                string sql = "CREATE TABLE IF NOT EXISTS DepositStatus" +
                                             "(Id int," +
                                             "Nome Varchar(50)," +
                                             "AgenciaDestino Varchar(10)," +
                                             "ContaDestino Varchar(10)," +
                                             "AgenciaOrigem Varchar(10)," +
                                             "ContaOrigem Varchar(10)," +
                                             "Valor Float," +
                                             "DataTransacao DateTime(10)," +
                                             "Status Varchar(10)," +
                                             "Razao Varchar(100))";
                sqlite_cmd = _sqliteConnection.CreateCommand();
                sqlite_cmd.CommandText = sql;
                sqlite_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar conexão com o banco de dados. Mensagem: {ex.Message}");
            }
        }
    }
}