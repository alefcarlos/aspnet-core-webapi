using System.Net;

namespace Framework.Services
{
    public sealed class ServicesResult
    {
        /// <summary>
        /// Indica se a operação foi bem sucedida
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Caso a operação não tenha sido bem sucedidade, aqui conterá a mensagem
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// HttpStatusCode que deve ser emitido
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        public object Data { get; set; }

        public ServicesResult(bool success, HttpStatusCode statusCode, string errorMessage = "")
        {
            Success = success;
            StatusCode = statusCode;
            Error = errorMessage;
        }

        public ServicesResult(bool success, HttpStatusCode statusCode, object data)
        {
            Success = success;
            StatusCode = statusCode;
            Data = data;
            Error = string.Empty;
        }


        public void SetData(object data) => Data = data;
    }
}
