using System;
using System.Collections.Generic;
using System.Text;

namespace LSC.Infra.Data.Entities
{
    public class Pecuarista : ILSCEntity
    {

        #region Construtores

        public Pecuarista()
        {
            Compras = new HashSet<CompraGado>();
        }

        #endregion

        #region Propriedades

        public int PecuaristaId { get; set; }

        public string Nome { get; set; }

        #endregion

        #region Lazy Navigation

        public virtual ICollection<CompraGado> Compras { get; set; }

        #endregion

    }
}
