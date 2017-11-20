using System;
using System.Linq;

namespace Lab2
{
    class Program
    {
        static public Output CreateOutput(Input input)
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

        static void Main(string[] args)
        {
            ISerializer serializer = new JsonSerializer();
            var host = "127.0.0.1";
            var port = int.Parse(Console.ReadLine());
            var htppClient = new Methods();

            var ping = htppClient.Ping(host, port);
            var inputData = htppClient.GetInputData(host, port);

            var newInput = serializer.Deserialize<Input>(inputData);
            var newOutput = CreateOutput(newInput);
            var serialObject = serializer.Serialize(newOutput);

            htppClient.WriteAnswer(host, port, serialObject);
        }
    }
}
