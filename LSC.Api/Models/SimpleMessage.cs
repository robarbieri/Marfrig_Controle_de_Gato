namespace LSC.Api.Models
{
    public class SimpleMessage : DefaultException
    {

        #region Construtores

        public SimpleMessage(string message, string detail) : base(message, detail)
        {
        }

        #endregion

    }
}