namespace Consignado.Api.Propostas
{
    public record Agente
    {
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
