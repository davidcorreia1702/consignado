namespace Consignado.HttpApi.Dominio.Entidade
{
    public record ConveniadaRestricao
    {
        public int IdConveniada { get; set; }
        public string Uf { get; set; }
        public decimal ValorLimite { get; set; }
    }
}
