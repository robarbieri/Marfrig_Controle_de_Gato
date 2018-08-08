using LSC.Cross.Interface;

namespace LSC.Cross.Transport
{
    public class CompraGadoItemTransport : ILSCTransport
    {

        #region Construtores

        public CompraGadoItemTransport() { }

        #endregion

        #region Propriedades

        public AnimalTransport Animal { get; set; }

        public int AnimalId { get; set; }

        public CompraGadoTransport CompraGado { get; set; }

        public int CompraGadoId { get; set; }

        public int CompraGadoItemId { get; set; }

        public decimal Preco { get; set; }

        public decimal Quantidade { get; set; }

        #endregion


    }
}