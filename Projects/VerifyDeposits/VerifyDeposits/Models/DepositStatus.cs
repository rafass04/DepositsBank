namespace VerifyDeposits.Models
{
    public class DepositStatus
    {
        public DepositStatus(string? id, string? nome, string? agenciaDestino, string? contaDestino, string? agenciaOrigem, string? contaOrigem, double valor, DateTime dataTransacao, string? status, string? razao)
        {
            Id = id;
            Nome = nome;
            AgenciaDestino = agenciaDestino;
            ContaDestino = contaDestino;
            AgenciaOrigem = agenciaOrigem;
            ContaOrigem = contaOrigem;
            Valor = valor;
            DataTransacao = dataTransacao;
            Status = status;
            Razao = razao;
        }

        public string? Id { get; set; }
        public string? Nome { get; set; }
        public string? AgenciaDestino { get; set; }
        public string? ContaDestino { get; set; }
        public string? AgenciaOrigem { get; set; }
        public string? ContaOrigem { get; set; }
        public double Valor { get; set; }
        public DateTime DataTransacao { get; set; }
        public string? Status { get; set; }
        public string? Razao { get; set; }
    }
}