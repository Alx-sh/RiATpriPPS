using System;
using System.Linq;

namespace Lab1
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
            ISerializer serializer;
            var serialType = Console.ReadLine().ToLower();
            var serialObject = Console.ReadLine();

            if (serialType == "json")
            {
                serializer = new JsonSerializer();
            }
            else
            {
                serializer = new XmlSerializer();
            }

            var newInput = serializer.Deserialize<Input>(serialObject);
            var newOutput = CreateOutput(newInput);
            serialObject = serializer.Serialize(newOutput);

            Console.WriteLine(serialObject.Replace(Environment.NewLine, "").Replace(" ", string.Empty));
            Console.ReadKey();
        }
    }
}
