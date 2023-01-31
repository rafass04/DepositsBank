namespace VerifyDeposits.Models
{
    public class Deposit
    {
        public Deposit(string? id, string? nome, string? agenciaDestino, string? contaDestino, string? agenciaOrigem, string? contaOrigem, double valor, DateTime dataTransacao)
        {
            Id = id;
            Nome = nome;
            AgenciaDestino = agenciaDestino;
            ContaDestino = contaDestino;
            AgenciaOrigem = agenciaOrigem;
            ContaOrigem = contaOrigem;
            Valor = valor;
            DataTransacao = dataTransacao;
        }

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