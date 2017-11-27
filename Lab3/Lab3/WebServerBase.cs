using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lab3
{
    public abstract class WebServerBase
    {
        protected HttpListenerContext context = null;
        protected JsonSerializer serializer;
        protected HttpListener httpListener;

        public WebServerBase()
        {
            serializer = new JsonSerializer();
        }

        public void Start(string host, int port)
        {
            httpListener = new HttpListener();

            httpListener.Prefixes.Add($"http://{host}:{port}/");
            httpListener.Start();
            Console.WriteLine("Start Listening");

            while (httpListener.IsListening)
            {
                context = httpListener.GetContext();

                var url = context.Request.RawUrl;
                var methodName = GetMethodName(url);

                ProcessQuery(methodName, httpListener);
            }
        }

        protected abstract void ProcessQuery(string methodName, HttpListener httpListener);

        protected T GetFromRequestBody<T>()
        {
            using (var streamReader = new StreamReader(context.Request.InputStream))
            {
                var bodyStr = streamReader.ReadToEnd();
                return serializer.Deserialize<T>(Encoding.UTF8.GetBytes(bodyStr));
            }
        }

        protected void WriteToResponse(HttpStatusCode httpStatusCode, string response)
        {
            context.Response.StatusCode = (int)httpStatusCode;
            using (var streamWriter = new StreamWriter(context.Response.OutputStream))
            {
                streamWriter.WriteLine(response);
            }
        }

        protected void WriteToResponse<T>(HttpStatusCode httpStatusCode, T response)
        {
            WriteToResponse(httpStatusCode, Encoding.UTF8.GetString(serializer.Serialize(response)));
        }

        protected static string GetMethodName(string url)
        {
            var result = url.Substring(1, url.Length - 1);
            var questionCharIndex = result.IndexOf("?", 0, StringComparison.Ordinal);
            if (questionCharIndex == -1)
            {
                return result;
            }

            return result.Substring(0, questionCharIndex);
        }
    }
}
