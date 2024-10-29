using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada
{
    public class RegraPorConveniada : Entity<Guid>
    {
        private RegraPorConveniada(){ }

        public RegraPorConveniada(Guid id, int conveniadaId, IValidarProposta regra)
        {
            Id = id;
            ConveiadaId = conveniadaId;
            Regra = regra;
        }

        public int ConveiadaId { get; }
        public IValidarProposta Regra{ get; }
    }
}
