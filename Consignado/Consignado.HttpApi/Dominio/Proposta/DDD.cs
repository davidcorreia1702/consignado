namespace Consignado.HttpApi.Dominio.Inscricao
{
    public class DDD
    {
        public string Numero { get; private set; }

        public DDD(string numero)
        {
            Numero = numero;
        }
    }
}
