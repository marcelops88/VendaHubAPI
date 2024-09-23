namespace Domain.Entities
{
    public class ItemVenda: EntityBase
    {
        public Guid ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal Desconto { get; private set; }

        public ItemVenda(Guid produtoId, int quantidade, decimal valorUnitario, decimal desconto)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            Desconto = desconto;
        }

        public decimal ValorTotal => (ValorUnitario * Quantidade) - Desconto;
    }

}
