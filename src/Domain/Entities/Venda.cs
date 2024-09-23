namespace Domain.Entities
{
    public class Venda: EntityBase
    {
        public int NumeroVenda { get; private set; }
        public DateTime DataVenda { get; private set; }
        public string NomeCliente { get; private set; }
        public string Filial { get; private set; }
        public List<ItemVenda> Itens { get; private set; }

        public Venda(int numeroVenda, string nomeCliente, string filial, List<ItemVenda> itens)
        {
            NumeroVenda = numeroVenda;
            NomeCliente = nomeCliente;
            Filial = filial;
            Itens = itens ?? new List<ItemVenda>();
            DataVenda = DateTime.UtcNow;
            Ativo = true;
        }

        public void Cancelar()
        {
            Ativo = false;
        }

        public decimal ValorTotal()
        {
            return Itens.Sum(item => item.ValorTotal);
        }
    }

}
