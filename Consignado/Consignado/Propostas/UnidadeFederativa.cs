namespace Consignado.Api.Propostas
{
    public class UnidadeFederativa : AgregateRoot
    {
        public string Sigla { get; private set; }
        public List<DDD> Ddds { get; set; }

        public UnidadeFederativa(string sigla)
        {
            Sigla = sigla;
        }
    }
}
