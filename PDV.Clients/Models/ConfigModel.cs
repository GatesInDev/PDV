namespace PDV.Clients.Models
{
    public class ConfigModel
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Impressora { get; set; }
        public byte[] Logo { get; set; } 
    }
}
