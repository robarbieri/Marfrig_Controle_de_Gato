using System;
using System.Collections.Generic;
using System.Linq;
using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Repository.Entities;

namespace LSC.Domain.Service
{

    public class CompraGadoItemService : ServiceBase<CompraGadoItemRepository, CompraGadoItemTransport, int>
    {

        #region Construtores

        public CompraGadoItemService(LSCContext ctx) : base(new CompraGadoItemRepository(ctx)) { }

        internal void RemoveAtCompra(int compraGadoId)
        {
            _repository.RemoveAtCompra(compraGadoId);
        }

        #endregion

    }

}
