namespace Domain.Entities
{
    public class Venda: EntityBase
    {
        public int NumeroVenda { get; private set; }
        public DateTime DataVenda { get; private set; }
        public string NomeCliente { get; private set; }
        public string CpfCliente { get; private set; }
        public string TelefoneCliente { get; private set; }
        public string EmailCliente { get; private set; }
        public string Filial { get; private set; }
        public List<ItemVenda> Itens { get; private set; }

        public Venda(int numeroVenda, string nomeCliente, string filial, List<ItemVenda> itens, string cpfCliente, string telefoneCliente, string emailCliente)
        {
            NumeroVenda = numeroVenda;
            NomeCliente = nomeCliente;
            Filial = filial;
            Itens = itens ?? new List<ItemVenda>();
            DataVenda = DateTime.UtcNow;
            Ativo = true;
            CpfCliente = cpfCliente;
            TelefoneCliente = telefoneCliente;
            EmailCliente = emailCliente;    
        }
        public void AtualizarVenda(int numeroVenda, string nomeCliente, string filial, List<ItemVenda> itens, string cpfCliente, string telefoneCliente, string emailCliente)
        {
            NumeroVenda = numeroVenda;
            NomeCliente = nomeCliente;
            Filial = filial;
            Itens = itens ?? new List<ItemVenda>();
            CpfCliente = cpfCliente;
            TelefoneCliente = telefoneCliente;
            EmailCliente = emailCliente;
            DataAtualizacao = DateTime.UtcNow;
        }

        public decimal ValorTotal()
        {
            return Itens.Sum(item => item.ValorTotal);
        }
    }

}
