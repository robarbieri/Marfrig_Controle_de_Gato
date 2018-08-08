namespace LSC.Api.Models
{
    public class DefaultException
    {

        #region Construtores

        public DefaultException() : this(null, null) { }

        public DefaultException(string message, string detail)
        {
            Message = message;
            MessageDetail = detail;
        }

        #endregion

        #region Propriedades

        public string Message { get; set; }

        public string MessageDetail { get; set; }

        #endregion

    }
}