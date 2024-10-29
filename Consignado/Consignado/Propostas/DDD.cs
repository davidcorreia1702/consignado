namespace Consignado.Api.Propostas
{
    public class DDD
    {
        public string Numero { get; private set; }
        public UnidadeFederativa Uf { get; set; }

        public DDD(string numero)
        {
            Numero = numero;
        }
    }
}
