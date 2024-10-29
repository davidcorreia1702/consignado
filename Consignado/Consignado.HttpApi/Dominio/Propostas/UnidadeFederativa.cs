namespace Consignado.HttpApi.Dominio.Propostas
{
    public class UnidadeFederativa
    {
        public string Sigla { get; private set; }
        public List<DDD> Ddds { get; set; }

        public UnidadeFederativa(string sigla)
        {
            Sigla = sigla;
        }
    }
}
