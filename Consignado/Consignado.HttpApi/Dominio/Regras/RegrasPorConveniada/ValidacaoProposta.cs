using Consignado.HttpApi.Dominio.Propostas;

namespace Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada
{
    public record ValidacaoProposta
    {
        public Conveniada Conveniada { get; set; }
        public TipoOperacao? TipoOperacao { get; set; }
        public string Uf { get; set; }
        public decimal? ValorOperacao { get; set; }
        public int? Prazo { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
