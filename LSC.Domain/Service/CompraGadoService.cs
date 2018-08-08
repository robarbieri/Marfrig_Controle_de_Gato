using LSC.Cross.Transport;
using LSC.Infra.Data;
using LSC.Infra.Data.Entities;
using LSC.Infra.Repository.Entities;
using System;
using System.Collections.Generic;

namespace LSC.Domain.Service
{

    public class CompraGadoService : ServiceBase<CompraGadoRepository, CompraGadoTransport, int>
    {

        #region Construtores

        public CompraGadoService(LSCContext ctx) : base(new CompraGadoRepository(ctx)) { }

        #endregion

        #region Métodos personalizados

        public void AddComplete(CompraGadoTransport transport)
        {

            if (transport.CompraGadoItens == null || transport.CompraGadoItens.Count.Equals(0))
                throw new Exception("A compra deve conter ao menos um item");

            _repository.Add(transport);
            CompraGadoItemService repItem = new CompraGadoItemService(_repository.Context());
            
            foreach (var item in transport.CompraGadoItens)
            {
                var e = new CompraGadoItemTransport()
                {
                    CompraGadoId = transport.CompraGadoId,
                    AnimalId = item.AnimalId,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade
                };

                repItem.Add(e);
            }
        }

        public IEnumerable<CompraGadoTransport> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return _repository.GetByPeriodo(dataInicial, dataFinal);
        }

        public IEnumerable<CompraGadoTransport> GetByPecuarista(int pecuaristaId)
        {
            return _repository.GetByPecuarista(pecuaristaId);
        }

        public void UpdateComplete(CompraGadoTransport transport)
        {

            if (transport.CompraGadoItens == null || transport.CompraGadoItens.Count.Equals(0))
                throw new Exception("A compra deve conter ao menos um item");

            _repository.Update(transport);
            CompraGadoItemService repItem = new CompraGadoItemService(_repository.Context());

            repItem.RemoveAtCompra(transport.CompraGadoId);

            foreach (var item in transport.CompraGadoItens)
            {
                var e = new CompraGadoItemTransport()
                {
                    CompraGadoId = transport.CompraGadoId,
                    AnimalId = item.AnimalId,
                    Preco = item.Preco,
                    Quantidade = item.Quantidade
                };

                repItem.Add(e);
            }
            
        }

        public void RemoveComplete(CompraGadoTransport transport)
        {
            (new CompraGadoItemService(_repository.Context())).RemoveAtCompra(transport.CompraGadoId);
            _repository.Remove(transport);

        }


        #endregion

    }

}
