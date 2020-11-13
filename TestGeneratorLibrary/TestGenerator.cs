using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestGeneratorLibrary
{
    public class TestGenerator
    {

        //content - source code of class(file)
        public TestInfo[] Generate(string content)
        {
            List<TestInfo> generatedTests = new List<TestInfo>();

            SyntaxNode treeRoot = CSharpSyntaxTree.ParseText(content).GetRoot();

            foreach (var userClass in treeRoot.DescendantNodes().OfType<ClassDeclarationSyntax>())
            {

                //structure of classFile:
                //Usings
                //namespace
                //className
                //Setup Method
                //Test methods for all public methods

                var classCode = SyntaxFactory.CompilationUnit();

                //AddNamespaces method


                //get the name of class
                string className = userClass.Identifier.ValueText;

                //TestNamespace
                NamespaceDeclarationSyntax NamespaceItem = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName("UnitTests"));

                //here we should process all public methods of current class
                //and create test for each method





                generatedTests.Add(new TestInfo(className, classCode.NormalizeWhitespace().ToFullString()));
            }


            return generatedTests.ToArray();
        }





        private UsingDirectiveSyntax[]  CreateUsings(SyntaxNode root)
        {
            //take out usings  form source code + source code namespace
            //add microsoft testing namespace
        }

        private MethodDeclarationSyntax CreateMethod(AttributeSyntax attribute, string methodName)
        {
            //create keyword name and body of method
        }

    }











}
