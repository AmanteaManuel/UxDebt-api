using System.Net;

namespace UxDebt.Response
{
    public class MultipleResponse<T> : Response
    {
        public List<T> Data { get; set; }

        public MultipleResponse<T> SetResponse(bool isSuccess, HttpStatusCode statusCode, string messege, List<T>? data)
        {

           this.Data = data;
           this.SetResponse(isSuccess, statusCode, messege);

            return this;
        }
    }
}
