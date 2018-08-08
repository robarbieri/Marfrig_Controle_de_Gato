using LSC.Cross.Interface;
using System.Collections.Generic;

namespace LSC.Cross.Transport
{
    public class PecuaristaTransport : ILSCTransport
    {

        #region Construtores

        public PecuaristaTransport()
        {
            Compras = new HashSet<CompraGadoTransport>();
        }

        #endregion

        #region Propriedades

        public ICollection<CompraGadoTransport> Compras { get; set; }

        public string Nome { get; set; }

        public int PecuaristaId { get; set; }

        #endregion

    }
}