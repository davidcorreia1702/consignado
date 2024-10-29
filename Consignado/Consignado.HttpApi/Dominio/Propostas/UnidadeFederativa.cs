namespace Consignado.HttpApi.Dominio.Propostas
{
    public class UnidadeFederativa
    {
        private ICollection<DDD> _ddds;
        public string Sigla { get; private set; }
        public string Nome { get; set; }
        public IEnumerable<DDD> Ddds => _ddds;
        public bool AssinaturaHibirda { get; set; }

        public UnidadeFederativa(string sigla, bool assinaturaHibrida)
        {
            Sigla = sigla;
            AssinaturaHibirda = assinaturaHibrida;
        }

        public void AdicionarDdd(DDD ddd)
        {
            if (_ddds == null)
                _ddds = new List<DDD>();

            _ddds.Add(ddd);
        }
    }
}
