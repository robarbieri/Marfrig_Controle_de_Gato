using System.Collections.Generic;

namespace LSC.Infra.Data.Entities
{
    public class Animal : ILSCEntity
    {

        #region Construtores

        public Animal()
        {
            CompraGadoItens = new HashSet<CompraGadoItem>();
        }

        #endregion

        #region Propriedades

        public int AnimalId { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        #endregion

        #region Lazy Navigation

        public virtual ICollection<CompraGadoItem> CompraGadoItens { get; set; }

        #endregion

    }

}
