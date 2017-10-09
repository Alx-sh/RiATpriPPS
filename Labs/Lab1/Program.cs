using System;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System.Text;
using Newtonsoft.Json;

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
            var newInput = new Input();
            var newOutput = new Output();

            var serialType = Console.ReadLine().ToLower();
            var serialObject = Console.ReadLine();

            switch (serialType)
            {
                case "json":
                    {
                        newInput = JsonConvert.DeserializeObject<Input>(serialObject);
                        newOutput = CreateOutput(newInput);

                        var outputString = JsonConvert.SerializeObject(newOutput);
                        Console.WriteLine(outputString.Replace(Environment.NewLine, string.Empty).Replace(" ", string.Empty));
                
                        break;
                    }
                case "xml":
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(Input));

                        using (var reader = new StringReader(serialObject))
                        {
                            newInput = (Input)formatter.Deserialize(reader);
                        }

                        newOutput = CreateOutput(newInput);
                        
                        formatter = new XmlSerializer(typeof(Output));
                        using (var stream = new MemoryStream())
                        {
                            formatter.Serialize(stream, newOutput);

                            var outputString = Encoding.UTF8.GetString(stream.ToArray());
                            outputString = outputString.Remove(0, outputString.IndexOf('>') + 1);
                            outputString = outputString.Remove(outputString.IndexOf(' '), outputString.IndexOf('>', outputString.IndexOf(' ')) - outputString.IndexOf(' '))
                                                .Replace(Environment.NewLine, string.Empty)
                                                .Replace(" ", string.Empty);

                            Console.WriteLine(outputString);
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Console.ReadKey();
        }
    }
}
