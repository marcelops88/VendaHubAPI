namespace API.DTOs.Requests
{
    public class VendaRequest
    {
        public int NumeroVenda { get; set; }
        public DateTime DataVenda { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EmailCliente { get; set; }
        public string Filial { get; set; }
        public List<ItemVendaRequest> Itens { get; set; }
    }
}
