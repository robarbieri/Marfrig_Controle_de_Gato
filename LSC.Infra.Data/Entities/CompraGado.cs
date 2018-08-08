using System;
using System.Collections.Generic;
using System.Text;

namespace LSC.Infra.Data.Entities
{
    public class CompraGado : ILSCEntity
    {

        #region Construtores

        public CompraGado()
        {
            CompraGadoItens = new HashSet<CompraGadoItem>();
        }

        #endregion

        #region Propriedades

        public int CompraGadoId { get; set; }

        public DateTime DataEntrega { get; set; }

        public int PecuaristaId { get; set; }

        #endregion

        #region Lazy Navigation

        public virtual Pecuarista Pecuarista { get; set; }

        public virtual ICollection<CompraGadoItem> CompraGadoItens { get; set; }

        #endregion

    }
}
