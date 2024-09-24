namespace API.DTOs.Requests
{
    public class ItemVendaRequest
    {
        public int ProdutoId { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public bool Cancelado { get; set; } 
    }
}
