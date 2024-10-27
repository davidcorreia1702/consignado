namespace Consignado.HttpApi.Dominio.Entidade
{
    public record Agente
    {
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
