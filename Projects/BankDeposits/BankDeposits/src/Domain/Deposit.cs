namespace BankDeposits.src.Domain
{
    public class Deposit
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? AgenciaDestino { get; set; }
        public string? ContaDestino { get; set; }
        public string? AgenciaOrigem { get; set; }
        public string? ContaOrigem { get; set; }
        public double Valor { get; set; }
        public DateTime DataTransacao { get; set; }
    }
}