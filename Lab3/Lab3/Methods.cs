using System;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Lab3
{
    public class Methods : WebServerBase, IMethods
    {
        public Input inputData;

        protected override void ProcessQuery(string methodName, HttpListener httpListener)
        {
            var methods = typeof(Methods).GetMethods(BindingFlags.Instance | BindingFlags.Public);
            foreach (var method in methods)
            {
                if (method.Name.Equals(methodName))
                {
                    method.Invoke(this, new object[0]);
                    return;
                }
            }
            WriteToResponse(HttpStatusCode.BadRequest, $"Unknown method {methodName}"); 
        }

        public void Ping()
        {
            WriteToResponse(HttpStatusCode.OK, bool.TrueString);
        }

        public void PostInputData()
        {
            inputData = GetFromRequestBody<Input>();
            WriteToResponse(HttpStatusCode.OK, inputData);
        }

        public void GetAnswer()
        {
            try
            {
                var outputData = CreateOutput(inputData);
                WriteToResponse(HttpStatusCode.OK, outputData);
            }
            catch (Exception)
            {
                WriteToResponse(HttpStatusCode.NotAcceptable, "Incorrect input data");
            }
        }

        public void Stop()
        {
            httpListener.Stop();
        }

        private Output CreateOutput(Input input)
        {
            var output = new Output();
            output.SumResult = input.Sums.Sum() * input.K;
            output.MulResult = input.Muls.Aggregate((p, x) => p *= x);

            var tmp = input.Sums.ToList();

            for (int i = 0; i < input.Muls.Length; i++)
            {
                tmp.Add(Convert.ToDecimal(input.Muls[i]));
            }

            tmp.Sort();
            output.SortedInputs = tmp.ToArray();

            return output;
        }
    }
}
