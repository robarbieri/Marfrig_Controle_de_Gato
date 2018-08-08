using LSC.Cross.Interface;
using System;
using System.Collections.Generic;

namespace LSC.Cross.Transport
{
    public class CompraGadoTransport : ILSCTransport
    {

        #region Construtores

        public CompraGadoTransport()
        {
            CompraGadoItens = new HashSet<CompraGadoItemTransport>();
        }

        #endregion

        #region Propriedades

        public int CompraGadoId { get; set; }

        public ICollection<CompraGadoItemTransport> CompraGadoItens { get; set; }

        public DateTime DataEntrega { get; set; }

        public PecuaristaTransport Pecuarista { get; set; }

        public int PecuaristaId { get; set; }

        #endregion

    }
}