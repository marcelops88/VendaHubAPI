namespace API.DTOs.Responses
{
    public class VendaResponse
    {
        public int NumeroVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EmailCliente { get; set; }
        public string Filial { get; set; }
        public decimal ValorTotalVenda { get; set; }
        public List<ItemVendaResponse> Itens { get; set; }
        public bool Cancelado { get; set; }
    }
}
