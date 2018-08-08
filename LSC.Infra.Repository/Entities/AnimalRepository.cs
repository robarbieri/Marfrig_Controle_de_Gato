using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;

namespace LSC.Infra.Repository.Entities
{
    public class AnimalRepository : RepositoryBase<Animal, AnimalTransport, int>
    {

        #region Construtores/Destrutores

        public AnimalRepository(LSCContext ctx) : base(ctx)
        { }

        #endregion

        #region Propriedades

        #endregion

        #region Overrides

        

        protected override Animal TransportToEntity(AnimalTransport transport)
        {
            return new Animal()
            {
                Descricao = transport.Descricao,
                Preco = transport.Preco
            };
        }
        
        protected override AnimalTransport EntityToTransport(Animal entity)
        {
            return ConvertToTransport(entity);
        }

        protected override Animal FindByKey(AnimalTransport transport)
        {
            return _ctx.Set<Animal>().Find(transport.AnimalId);
        }

        protected override void UpdateFieldByTransport(Animal entity, AnimalTransport transport)
        {
            entity.Descricao = transport.Descricao;
            entity.Preco = transport.Preco;
        }

        protected override void CatchId(Animal entity, AnimalTransport transport)
        {
            transport.AnimalId = entity.AnimalId;
        }

        #endregion

        #region Métodos Estáticos
        public static AnimalTransport ConvertToTransport(Animal entity)
        {

            if (entity == null)
                return null;

            return new AnimalTransport()
            {
                AnimalId = entity.AnimalId,
                Descricao = entity.Descricao,
                Preco = entity.Preco
            };

        }


        #endregion

    }

}
