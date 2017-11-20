using System.Net;

namespace Lab2
{
    class Methods : WebClientBase, IMethods
    {
        HttpStatusCode status;

        public bool Ping(string host, int port)
        {
            SendHttpRequest("GET", host, port, "Ping", out status);
            return (status == HttpStatusCode.OK) ? true : false;
        }

        public byte[] GetInputData(string host, int port)
        {
            return SendHttpRequest("GET", host, port, "GetInputData", out status);
        }

        public void WriteAnswer(string host, int port, byte[] requestBody)
        {
            SendHttpRequest("POST", host, port, "WriteAnswer", out status, requestBody);
        }
    }
}
