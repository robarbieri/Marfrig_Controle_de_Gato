using LSC.Cross.Interface;
using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LSC.Infra.Repository.Entities
{
    public class CompraGadoRepository : RepositoryBase<CompraGado, CompraGadoTransport, int>
    {

        #region Construtores

        public CompraGadoRepository(LSCContext ctx) : base(ctx) { }

        #endregion

        #region Overrides

        protected override void CatchId(CompraGado entity, CompraGadoTransport transport)
        {
            transport.CompraGadoId = entity.CompraGadoId;
        }

        protected override CompraGadoTransport EntityToTransport(CompraGado entity)
        {

            if (entity == null)
                return null;

            ICollection<CompraGadoItemTransport> items = new List<CompraGadoItemTransport>();
            if (entity.CompraGadoItens != null && entity.CompraGadoItens.Count > 0)
            {
                foreach (var i in entity.CompraGadoItens)
                {
                    items.Add(CompraGadoItemRepository.ConvertToTransport(i));
                }
            }

            return new CompraGadoTransport()
            {
                CompraGadoId = entity.CompraGadoId,
                DataEntrega = entity.DataEntrega,
                PecuaristaId = entity.PecuaristaId,
                Pecuarista = (entity.Pecuarista == null ? null : new PecuaristaTransport()
                {
                    PecuaristaId = entity.PecuaristaId,
                    Nome = entity.Pecuarista.Nome
                }),
                CompraGadoItens = items
            };
        }

        public IEnumerable<CompraGadoTransport> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return EntityToTransport(_ctx.CompraGado.Where(x => x.DataEntrega >= dataInicial && x.DataEntrega <= dataFinal).ToList());
        }

        protected override CompraGado FindByKey(CompraGadoTransport transport)
        {
            return _ctx.Set<CompraGado>().Find(transport.CompraGadoId);
        }

        protected override CompraGado TransportToEntity(CompraGadoTransport transport)
        {
            return new CompraGado()
            {
                DataEntrega = transport.DataEntrega,
                PecuaristaId = transport.PecuaristaId
            };
        }

        protected override void UpdateFieldByTransport(CompraGado entity, CompraGadoTransport transport)
        {
            entity.DataEntrega = transport.DataEntrega;
            entity.PecuaristaId = transport.PecuaristaId;
        }

        #endregion

        #region Métodos Personalizados

        public IEnumerable<CompraGadoTransport> GetByPecuarista(int pecuaristaId)
        {
            return EntityToTransport(_ctx.Set<CompraGado>().Where(x => x.PecuaristaId.Equals(pecuaristaId)).ToList());
        }

        #endregion

    }

}
