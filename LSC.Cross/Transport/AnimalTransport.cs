using LSC.Cross.Interface;
using System.Collections.Generic;

namespace LSC.Cross.Transport
{
    public class AnimalTransport : ILSCTransport
    {

        #region Construtores

        public AnimalTransport()
        {
            CompraGadoItens = new HashSet<CompraGadoItemTransport>();
        }

        #endregion

        #region Propriedades

        public int AnimalId { get; set; }

        public ICollection<CompraGadoItemTransport> CompraGadoItens { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        #endregion

    }
}
