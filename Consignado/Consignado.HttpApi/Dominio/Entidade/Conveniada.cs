namespace Consignado.HttpApi.Dominio.Entidade
{
    public class Conveniada
    {
        public Conveniada()
        {
            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public bool AceitaRefinanciamento { get; set; }
        public ICollection<ConveniadaRestricao> Restricoes { get; set; }
    }
}
