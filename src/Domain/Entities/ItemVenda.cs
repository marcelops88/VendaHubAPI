namespace Domain.Entities
{
    public class ItemVenda : EntityBase
    {
        public int ProdutoId { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal Desconto { get; private set; }
        public bool Cancelado { get; private set; }

        public ItemVenda(int produtoId, string nomeProduto, int quantidade, decimal valorUnitario, decimal desconto, bool cancelado)
        {
            ProdutoId = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            Desconto = desconto;
            Cancelado = cancelado;
        }

        public void Cancelar()
        {
            Cancelado = true;
        }

        public decimal ValorTotal => (ValorUnitario * Quantidade) - Desconto;
    }

}
