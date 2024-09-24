namespace API.DTOs.Responses
{
    public class ItemVendaResponse
    {
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotalItem => (ValorUnitario * Quantidade) - Desconto;
    }
}
