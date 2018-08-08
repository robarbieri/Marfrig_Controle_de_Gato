using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Repository.Entities;

namespace LSC.Domain.Service
{
    public class AnimalService : ServiceBase<AnimalRepository, AnimalTransport, int>
    {

        #region Construtores

        public AnimalService(LSCContext ctx) : base(new AnimalRepository(ctx)) { }

        #endregion

    }
}
