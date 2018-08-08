using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;

namespace LSC.Infra.Repository.Entities
{
    public class PecuaristaRepository : RepositoryBase<Pecuarista, PecuaristaTransport, int>
    {

        #region Construtores

        public PecuaristaRepository(LSCContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        protected override void CatchId(Pecuarista entity, PecuaristaTransport transport)
        {
            transport.PecuaristaId = entity.PecuaristaId;
        }

        protected override PecuaristaTransport EntityToTransport(Pecuarista entity)
        {
            return ConvertToTransport(entity);
        }

        protected override Pecuarista FindByKey(PecuaristaTransport transport)
        {
            return _ctx.Set<Pecuarista>().Find(transport.PecuaristaId);
        }

        protected override Pecuarista TransportToEntity(PecuaristaTransport transport)
        {
            return new Pecuarista()
            {
                Nome = transport.Nome
            };
        }

        protected override void UpdateFieldByTransport(Pecuarista entity, PecuaristaTransport transport)
        {
            entity.Nome = transport.Nome;
        }

        #endregion

        #region Métodos Estáticos

        public static PecuaristaTransport ConvertToTransport(Pecuarista entity)
        {

            if (entity == null)
                return null;

            return new PecuaristaTransport()
            {
                PecuaristaId = entity.PecuaristaId,
                Nome = entity.Nome
            };

        }

        #endregion

    }
}
