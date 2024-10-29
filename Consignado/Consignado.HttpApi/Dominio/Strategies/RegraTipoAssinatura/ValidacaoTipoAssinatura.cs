namespace Consignado.HttpApi.Dominio.Strategies.RegraTipoAssinatura
{
    public record ValidacaoTipoAssinatura
    {
        public string UfDdd { get; set; }
        public string UfNascimento { get; set; }
        public bool AssinaturaHibirda { get; set; }
    }
}
