using System.Net;

namespace UxDebt.Response
{
    public abstract class Response 
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode ResponseCode { get; set; }
        public string Message { get; set; }


        public Response SetResponse(bool isSuccess, HttpStatusCode statusCode, string messege)
        {
            this.ResponseCode = statusCode;
            this.Message = messege;
            this.IsSuccess = isSuccess;

            return this;
        }
    }
}
