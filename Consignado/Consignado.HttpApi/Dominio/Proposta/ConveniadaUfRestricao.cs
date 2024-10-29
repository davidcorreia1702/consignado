namespace Consignado.HttpApi.Dominio.Inscricao
{
    public record ConveniadaUfRestricao
    {
        public int ConveiadaID { get; set; }
        public string UF { get; set; }
        public Conveniada Conveniada { get; set; }
        public UnidadeFederativa UnidadeFederativa { get; set; }

        public decimal ValorLimite { get; set; }

        protected ConveniadaUfRestricao()
        {
            
        }

        public ConveniadaUfRestricao(string uf, decimal valorLimite)
        {
            UF = uf;
            ValorLimite = valorLimite;
        }
    }
}
