using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    class Program
    {

        static string CD;

        static void Main(string[] args)
        {
            CD = System.IO.Directory.GetCurrentDirectory();
            if (args.Length == 0)
            {
                writeHeader();
                bool finish = false;
                do
                {
                    string[] commands = getCommands();
                    finish = runCommand(commands);
                } while (!finish);
            } else if (args.Length > 0)
            {

            }
        }

        static void writeHeader()
        {
            Console.WriteLine("IFEScript Interpreter v.01 (c) 2017 Daniel Valcour");
            Console.WriteLine("Please type a command to continue, or type help for a list of commands.");
            Console.WriteLine("");
        }

        static string[] getCommands()
        {
            List<string> commands = new List<string>();
            Console.Write("<" + CD + "> ");
            string input = Console.ReadLine();

            string tok = "";
            for (int x = 0; x < input.Length; x ++)
            {
                if (input[x].ToString() == " " && (commands.Count == 0 || commands[commands.Count-1].ToLower() != "cd")) {
                    if (tok != "")
                    {
                        commands.Add(tok);
                        tok = "";
                    }
                }
                else
                {
                    tok += input[x].ToString();
                }
            }

            if (tok != "")
            {
                commands.Add(tok);
                tok = "";
            }

            return commands.ToArray();
        }

        static bool checkPath(string toAdd)
        {
            return System.IO.File.Exists(System.IO.Path.Combine(CD, toAdd));
        }

        static string getFullPath(string toAdd)
        {
            return System.IO.Path.Combine(CD, toAdd);
        }

        static bool checkFileType(string toAdd)
        {
            return (".ife" == System.IO.Path.GetExtension(getFullPath(toAdd)));
        }

        static bool runCommand(string[] commands)
        {
            ////debug loop to check commands
            //for (int x = 0; x < args.Length; x ++)
            //{
            //    Console.WriteLine(args[x]);
            //}

            if (commands.Length == 1)
            {
                if (commands[0].ToLower() == "help")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Here are a list of commands:");
                    Console.WriteLine("help          Display this list.");
                    Console.WriteLine("license       Display the license.");
                    Console.WriteLine("copyright     Display the copyright info");
                    Console.WriteLine("credits       Display a list of people who helped develop the language.");
                    Console.WriteLine("about         Display information about the language.");
                    Console.WriteLine("quit          Terminates this program.");
                    Console.WriteLine("cd <dir>      Changes the current directory.");
                    Console.WriteLine("run <file>    Runs the given file at the current directory.");
                    Console.WriteLine("debug <file>  Debugs the given file at the current directory.");
                    Console.WriteLine("");
                }
                else if (commands[0].ToLower() == "license")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Copyright (c) 2017 Daniel Valcour");
                    Console.WriteLine("");
                    Console.WriteLine("Permission is hereby granted, free of charge, to any person obtaining a copy");
                    Console.WriteLine("of this software and associated documentation files (the \"Software\"), to deal");
                    Console.WriteLine("in the Software without restriction, including without limitation the rights");
                    Console.WriteLine("to use, copy, modify, merge, publish, distribute, sublicense, and/or sell");
                    Console.WriteLine("copies of the Software, and to permit persons to whom the Software is");
                    Console.WriteLine("furnished to do so, subject to the following conditions:");
                    Console.WriteLine("");
                    Console.WriteLine("The above copyright notice and this permission notice shall be included in all");
                    Console.WriteLine("copies or substantial portions of the Software.");
                    Console.WriteLine("");
                    Console.WriteLine("THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR");
                    Console.WriteLine("IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,");
                    Console.WriteLine("FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE");
                    Console.WriteLine("AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER");
                    Console.WriteLine("LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,");
                    Console.WriteLine("OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE");
                    Console.WriteLine("SOFTWARE.");
                    Console.WriteLine("");
                }
                else if (commands[0].ToLower() == "copyright")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Copyright (c) 2017 Daniel Valcour");
                    Console.WriteLine("");
                }
                else if (commands[0].ToLower() == "credits")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Credits:");
                    Console.WriteLine("Daniel Valcour (Haravin)");
                    Console.WriteLine("");
                }
                else if (commands[0].ToLower() == "about")
                {
                    Console.WriteLine("");
                    Console.WriteLine("About:");
                    Console.WriteLine("IFEscript is a programming langauge initially developed by Daniel Valcour. The ");
                    Console.WriteLine("language's purpose is for the development of interactive fiction video games.");
                    Console.WriteLine("The language will have a built in natural language parser that can interpret");
                    Console.WriteLine("commands given by users. It will also be able to save any game from any");
                    Console.WriteLine(" point to a playable file.");
                    Console.WriteLine("");
                }
                else if (commands[0].ToLower() == "quit")
                {
                    return true;
                }
                else if (commands[0].ToLower() == "cd" || commands[0].ToLower() == "run" || commands[0].ToLower() == "debug")
                {
                    Console.WriteLine("");
                    Console.WriteLine("\"" + commands[0] + "\" requires more arguments.");
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("\"" + commands[0] + "\" is and invalid command.");
                    Console.WriteLine("");
                }
            }
            else if (commands.Length == 2)
            {
                if (commands[0].ToLower() == "cd")
                {
                    if (System.IO.Directory.Exists(commands[1]) && !System.IO.File.Exists(commands[1]))
                    {
                        CD = commands[1];
                        System.IO.Directory.SetCurrentDirectory(CD);
                    }
                    else if (System.IO.File.Exists(commands[1]))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Enter a directory, not a file path!");
                        Console.WriteLine("");
                    }             
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Invalid File Path!");
                        Console.WriteLine("");
                    }
                }
                else if (commands[0].ToLower() == "run")
                {
                    if (checkPath(commands[1]))
                    {
                        if (checkFileType(commands[1]))
                        {
                            string path = getFullPath(commands[1]);
                            IFE.Intperpreter.Interpret(path, false);
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Wrong file extension!");
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("File doesn't exist!");
                        Console.WriteLine("");
                    }

                }
                else if (commands[0].ToLower() == "debug")
                {
                    if (checkPath(commands[1]))
                    {
                        if (checkFileType(commands[1]))
                        {
                            string path = getFullPath(commands[1]);
                            IFE.Intperpreter.Interpret(path, true);
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Wrong file extension!");
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("File doesn't exist!");
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("\"" + commands[0] + "\" is and invalid command.");
                    Console.WriteLine("");
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("There are no commands avaliable that have " + commands.Count().ToString() + " arguments!");
                Console.WriteLine("");
            }

            return false;
        }
    }
}
