using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Repository.Entities;

namespace LSC.Domain.Service
{
    public class PecuaristaService : ServiceBase<PecuaristaRepository, PecuaristaTransport, int>
    {

        #region Construtores

        public PecuaristaService(LSCContext ctx) : base(new PecuaristaRepository(ctx)) { }

        #endregion

    }
}
