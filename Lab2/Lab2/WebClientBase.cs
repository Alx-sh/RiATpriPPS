using System.IO;
using System.Net;
using System.Text;

namespace Lab2
{
    public abstract class WebClientBase
    {
        protected byte[] SendHttpRequest(string httpMethod, string host, int port, string method, out HttpStatusCode status, byte[] requestBody = null)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create($"http://{host}:{port}/{method}");
            webRequest.Method = httpMethod;
            webRequest.Timeout = 1000;

            if (requestBody != null && requestBody.Length > 0)
            {
                using (var sw = new StreamWriter(webRequest.GetRequestStream()))
                {
                    sw.Write(Encoding.UTF8.GetString(requestBody));
                }
            }

            var response = (HttpWebResponse)webRequest.GetResponse();
            status = response.StatusCode;

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var responseString = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                return responseString;
            }
        }
    }
}
