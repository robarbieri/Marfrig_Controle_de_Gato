using System;
using System.Collections.Generic;
using System.Text;

namespace LSC.Infra.Data.Entities
{
    public class CompraGadoItem : ILSCEntity
    {

        #region Propriedades

        public int CompraGadoItemId { get; set; }

        public int CompraGadoId { get; set; }

        public int AnimalId { get; set; }

        public decimal Quantidade { get; set; }

        public decimal Preco { get; set; }

        #endregion

        #region Lazy Navigation

        public virtual CompraGado CompraGado { get; set; }

        public virtual Animal Animal { get; set; }

        #endregion

    }
}
