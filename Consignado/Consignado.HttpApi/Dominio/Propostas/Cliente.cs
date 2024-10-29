namespace Consignado.HttpApi.Dominio.Propostas
{
    public record Cliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public bool Bloqueado { get; set; }
    }
}
