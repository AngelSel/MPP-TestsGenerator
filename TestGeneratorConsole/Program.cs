using System.Collections.Generic;
using System.IO;
using TestGeneratorLibrary;

namespace TestGeneratorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string writePath = @"";
            string path = @"";
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


        }
    }
}
