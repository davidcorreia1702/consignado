namespace Consignado.HttpApi.Dominio.Inscricao
{
    public record Agente
    {
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
