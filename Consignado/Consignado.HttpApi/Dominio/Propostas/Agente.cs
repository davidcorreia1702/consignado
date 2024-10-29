namespace Consignado.HttpApi.Dominio.Propostas
{
    public record Agente
    {
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
