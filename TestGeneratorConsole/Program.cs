using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestGeneratorLibrary;

namespace TestGeneratorConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {/*
            string writePath = @"result.txt";
            string path = @"ListGenerator.cs";
            string resultstr = "";
            List<TestInfo> result = new List<TestInfo>();

            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    resultstr += line;
                }
            }

            TestGenerator gen = new TestGenerator();
            TestInfo[]  info =  gen.Generate(resultstr);

            foreach (var item in info)
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine(item.TestCode);
                }
            }
            */

            string[] files;
            files = Directory.GetFiles(@"TestData\", "*.cs");

            var configure = new PipelineConfiguration(2, 2, 2);
            var pipeline = new Pipeline(configure);
            await pipeline.Processing(files);




        }
    }
}
