using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestGeneratorLibrary;

namespace TestGeneratorConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            string[] files;
            files = Directory.GetFiles(@"TestData\", "*.cs");

            var configure = new PipelineConfiguration(2, 2, 2);
            var pipeline = new Pipeline(configure);

            string outputDirectory = Directory.GetCurrentDirectory() + @"\TestResult\";

            await pipeline.Processing(files,outputDirectory);


        }
    }
}
