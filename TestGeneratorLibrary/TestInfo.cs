using System;
using System.Collections.Generic;
using System.Text;

namespace TestGeneratorLibrary
{
    public class TestInfo
    {
        public string TestName { get; set; }
        public string TestCode { get; set; }

        public TestInfo(string name, string code)
        {
            TestName = name;
            TestCode = code;
        }

    }
}
