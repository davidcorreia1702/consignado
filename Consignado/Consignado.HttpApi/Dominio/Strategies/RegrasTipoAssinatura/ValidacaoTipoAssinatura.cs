namespace Consignado.HttpApi.Dominio.Strategies.RegrasTipoAssinatura
{
    public record ValidacaoTipoAssinatura
    {
        public string UfDdd { get; set; }
        public string UfNascimento { get; set; }
        public bool AssinaturaHibirda { get; set; }
    }
}
