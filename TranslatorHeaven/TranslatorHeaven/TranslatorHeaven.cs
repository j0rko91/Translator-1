using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Diagnostics;

namespace TranslatorHeaven
{
    class TranslatorHeaven
    {
        static void Main(string[] args)
        {
            //reading file
            string fileToRead = ("../../Test/example.scs");
            string[] inputFile = File.ReadAllLines(fileToRead);
            int inputFileLines = File.ReadAllLines(fileToRead).Length;
            
            //user can input the end-result file`s name
            Console.WriteLine("Enter name for the new file: ");
            string fileName = Console.ReadLine();
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "result";
            }

            //making a copy in a .cs format
            using (StreamWriter writer = new StreamWriter("../../Test/"+ fileName + ".cs"))
            {
                for (int line = 0; line < inputFileLines; line++)
                {
                    writer.WriteLine(inputFile[line]);
                }
            }

            string fileToCompile = "../../Test/" + fileName + ".cs";
            string output = string.Format("{0}.exe", fileName);

            //doesnt create file
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
            ICodeCompiler icc = codeProvider.CreateCompiler();
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = output;
            CompilerResults compileResults = codeProvider.CompileAssemblyFromFile(parameters, fileToCompile);


            Console.WriteLine();
            Process.Start(output);
            
        }
    }
}
