using System;
using LSC.Cross.Interface;
using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;
using System.Linq;
using System.Collections.Generic;

namespace LSC.Infra.Repository.Entities
{
    public class CompraGadoItemRepository : RepositoryBase<CompraGadoItem, CompraGadoItemTransport, int>
    {

        #region Construtores

        public CompraGadoItemRepository(LSCContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        protected override void CatchId(CompraGadoItem entity, CompraGadoItemTransport transport)
        {
            transport.CompraGadoItemId = entity.CompraGadoItemId;
        }

        public void RemoveAtCompra(int compraGadoId)
        {

            IEnumerable<CompraGadoItem> items = _ctx.Set<CompraGadoItem>().Where(x => x.CompraGadoId.Equals(compraGadoId));
            foreach (CompraGadoItem item in items)
            {
                Remove(new CompraGadoItemTransport() { CompraGadoItemId = item.CompraGadoItemId });
            }

        }

        protected override CompraGadoItemTransport EntityToTransport(CompraGadoItem entity)
        {
            return ConvertToTransport(entity);
        }

        protected override CompraGadoItem FindByKey(CompraGadoItemTransport transport)
        {
            return _ctx.Set<CompraGadoItem>().Find(transport.CompraGadoItemId);
        }

        protected override CompraGadoItem TransportToEntity(CompraGadoItemTransport transport)
        {
            return new CompraGadoItem()
            {
                CompraGadoId = transport.CompraGadoId,
                AnimalId = transport.AnimalId,
                Quantidade = transport.Quantidade,
                Preco = transport.Preco
            };
        }

        protected override void UpdateFieldByTransport(CompraGadoItem entity, CompraGadoItemTransport transport)
        {
            entity.CompraGadoId = transport.CompraGadoId;
            entity.AnimalId = transport.AnimalId;
            entity.Quantidade = transport.Quantidade;
            entity.Preco = transport.Preco;
        }

        #endregion

        #region Métodos Estáticos

        public static CompraGadoItemTransport ConvertToTransport(CompraGadoItem entity)
        {

            if (entity == null)
                return null;

            return new CompraGadoItemTransport()
            {
                CompraGadoItemId = entity.CompraGadoItemId,
                CompraGadoId = entity.CompraGadoId,
                AnimalId = entity.AnimalId,
                Quantidade = entity.Quantidade,
                Preco = entity.Preco,
                Animal = AnimalRepository.ConvertToTransport(entity.Animal)
            };

        }

        #endregion

    }

}
