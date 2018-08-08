namespace LSC.WebApi.Client.Entity
{

    public class DefaultResponse
    {

        public string Message { get; set; }

        public string MessageDetail { get; set; }

    }

    public class ApiPutResponse : DefaultResponse { }

    public class ApiDeleteResponse : DefaultResponse { }

    public class ApiErrorResponse : DefaultResponse { }

}
