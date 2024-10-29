namespace Consignado.HttpApi.Dominio.Inscricao
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
