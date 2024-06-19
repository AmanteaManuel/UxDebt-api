using System.Net;

namespace UxDebt.Response
{
    public class SingleResponse<T> : Response
    {       
        public T Data { get; set; }

        public SingleResponse<T> SetResponse(bool isSuccess, HttpStatusCode statusCode, string messege, T? data)
        {

            this.Data = data;
            this.SetResponse(isSuccess, statusCode, messege);

            return this;
        }

    }
}
