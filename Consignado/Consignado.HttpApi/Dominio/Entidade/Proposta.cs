namespace Consignado.HttpApi.Dominio.Entidade
{
    public class Proposta
    {
        public Proposta()
        {
            
        }

        public string Cliente { get; set; }
        public string CpfAgente { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Matricula { get; set; }
        public decimal ValorRendimento { get; set; }
        public string CodigoConveniada { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public string Prazo { get; set; }
        public decimal ValorOperacao { get; set; }
        public decimal ValorPrestacao { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public TipoAssinatura TipoAssinatura { get; set; }
    }

    public enum TipoOperacao { Novo, Portabilidade, Refinanciamento }
    public enum TipoAssinatura { Eletronica, Hibrida, Figital }
}
