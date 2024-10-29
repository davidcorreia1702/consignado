namespace Consignado.HttpApi.Dominio.Propostas
{
    public class DDD
    {
        public int Id { get; set; }
        public string Numero { get; private set; }
        public int UnidadeFederativaId { get; private set; }

        public DDD(string numero)
        {
            Numero = numero;
        }
    }
}
