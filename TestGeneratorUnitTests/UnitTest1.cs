using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using TestGeneratorLibrary;

namespace TestGeneratorUnitTests
{
    [TestClass]
    public class UnitTest1
    {

        private TestInfo[] result;

        [TestInitialize]
        public void Setup()
        {
            string fileContent;
            using (StreamReader fStream = new StreamReader(@"ListGenerator.cs"))
            {
                fileContent = fStream.ReadToEnd();
            }
            result = new TestGenerator().Generate(fileContent);
        }

        [TestMethod]
        public void TestResultCounts()
        {
            Assert.AreEqual(result.Length,1);

        }

        [TestMethod]
        public void TestUsings()
        {
            string[] usingsNames = CSharpSyntaxTree.ParseText(result[0].TestCode).
                GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>().
                Select(item => item.Name.ToString()).ToArray();

            Assert.AreEqual(usingsNames.Length, 5);

            string[] expectedUsings = { "System", "System.Collections", "System.Collections.Generic", "FakerLibrary", "Microsoft.VisualStudio.TestTools.UnitTesting" };

            for (int i = 0; i < expectedUsings.Length; i++)
                Assert.AreEqual(expectedUsings[i], usingsNames[i]);
        }

        [TestMethod]
        public void TestNamespaces()
        {
            string[] namespaces = CSharpSyntaxTree.ParseText(result[0].TestCode).
                GetRoot().DescendantNodes().OfType<NamespaceDeclarationSyntax>().
                Select(item => item.Name.ToString()).ToArray();

            Assert.AreEqual(namespaces.Length, 1);
            Assert.AreEqual(namespaces[0], "UnitTests");

        }


        [TestMethod]
        public void TestClassesName()
        {
            string[] classes = CSharpSyntaxTree.ParseText(result[0].TestCode).
               GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().
               Select(item => item.Identifier.ValueText).ToArray();

            Assert.AreEqual(classes.Length, 1);
            Assert.AreEqual(classes[0], "ListGeneratorTest");
        }

        [TestMethod]
        public void TestMethodsCount()
        {
            var methods = CSharpSyntaxTree.ParseText(result[0].TestCode).
                GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();

            Assert.AreEqual(methods.Count(), 2);
        }

        [TestMethod]
        public void TestMethodsSignature()
        {
            var methods = CSharpSyntaxTree.ParseText(result[0].TestCode).
                GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();

            foreach (MethodDeclarationSyntax method in methods)
            {
                Assert.AreEqual(method.ReturnType.ToString(), "void");
                Assert.IsTrue(method.Modifiers.Any(SyntaxKind.PublicKeyword));
                Assert.AreEqual(method.AttributeLists[0].Attributes[0].Name.ToString(), "TestMethod");
            }
        }


    }
}
