namespace Consignado.HttpApi.Dominio.Entidade
{
    public class Cliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Bloqueado { get; set; }
    }
}
