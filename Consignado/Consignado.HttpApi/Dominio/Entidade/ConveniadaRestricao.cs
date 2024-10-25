namespace Consignado.HttpApi.Dominio.Entidade
{
    public class ConveniadaRestricao
    {
        public int IdConveniada { get; set; }
        public string Uf { get; set; }
        public decimal ValorLimite { get; set; }
    }
}
