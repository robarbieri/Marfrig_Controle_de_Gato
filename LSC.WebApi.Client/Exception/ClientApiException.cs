using System.Runtime.Serialization;
using System.Security;

namespace LSC.WebApi.Client.Exception
{


    public class ClientApiException : System.Exception
    {

        #region Construtores

        public ClientApiException() : base() { }

        public ClientApiException(string message) : base(message) { }

        public ClientApiException(string message, System.Exception innerException) : base(message, innerException) { }

        [SecuritySafeCritical]
        protected ClientApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

    }

}
