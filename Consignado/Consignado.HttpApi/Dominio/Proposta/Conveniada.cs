namespace Consignado.HttpApi.Dominio.Inscricao
{
    public record Conveniada
    {
        private ICollection<ConveniadaUfRestricao> _restricoes;
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Codigo { get; private set; }
        public bool AceitaRefinanciamento { get; private set; }
        public IEnumerable<ConveniadaUfRestricao> Restricoes => _restricoes;

        public Conveniada(int id, string nome, string codigo, bool aceitaRefinanciamento)
        {
            Id = id;
            Nome = nome;
            Codigo = codigo;
            AceitaRefinanciamento = aceitaRefinanciamento;
        }

        public void AdicionarRestricao(ConveniadaUfRestricao restricao)
        {
            if (_restricoes == null)
                _restricoes = new List<ConveniadaUfRestricao>();

            _restricoes.Add(restricao);
        }
    }
}
