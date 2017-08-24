using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    class Intperpreter
    {
        public static void Interpret(string path, bool debug)
        {
            if (debug)
            {
                Console.WriteLine("");
                Console.WriteLine("<<debug>> Interpreting New Code Block.");
                Console.WriteLine("<<debug>> Reading File Contents.");
                Console.WriteLine("");
            }

            //read file contents
            string contents = System.IO.File.ReadAllText(path);

            if (debug)
            {
                Console.WriteLine("<<debug>> Printing file contents.");
                Console.WriteLine(contents);
                Console.WriteLine("<<debug>> Finished printing file contents.");
                Console.WriteLine("<<debug>> Slicing file contents.");
            }

            //slicing stage
            string[] data = IFE.Slicer.Slice(contents);

            if (debug)
            {
                Console.WriteLine("<<debug>> Printing sliced data.");
                for (int x = 0; x < data.Length; x ++)
                {
                    Console.WriteLine(data[x]);
                }
                Console.WriteLine("<<debug>> Finished printing sliced data.");
                Console.WriteLine("<<debug>> Lexing sliced data.");
            }

            List<Statement> statements = IFE.Lexer.Lex(data);

            if (debug)
            {
                Console.WriteLine("<<debug>> Printing lexed statements.");
                for (int x = 0; x < statements.Count; x ++)
                {
                    Console.WriteLine(statements[x].ToString());
                }
                Console.WriteLine("<<debug>> Finished printing lexed statements.");
            }
        }
    }
}
