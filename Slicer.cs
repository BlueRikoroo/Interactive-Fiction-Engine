using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    class Slicer
    {
        public static string[] Slice(string contents)
        {
            List<string> data = new List<string>();

            string tok = "";
            bool inSingleLineComment = false;
            bool inMultiLineComment = false;
            bool inString = false;
            for (int x = 0; x < contents.Length; x ++)
            {
                if (!inSingleLineComment)
                {
                    if (!inMultiLineComment)
                    {
                        if (!inString)
                        {
                            if (Char.IsWhiteSpace(contents[x]))
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                            }
                            else if (contents[x].ToString() == ";" || contents[x].ToString() == ":" || contents[x].ToString() == "(" || contents[x].ToString() == ")" || contents[x].ToString() == "{" || contents[x].ToString() == "}" || contents[x].ToString() == "[" || contents[x].ToString() == "]")
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                data.Add(contents[x].ToString());
                            }
                            else if (contents[x].ToString() == "\"")
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                inString = true;
                            }
                            else if (CheckDouble(contents, x, "/", "/"))
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                inSingleLineComment = true;
                            }
                            else if (CheckDouble(contents, x, "/", "*"))
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                inMultiLineComment = true;
                            }
                            else if (CheckDouble(contents, x, "+", "=") || CheckDouble(contents, x, "-", "=") || CheckDouble(contents, x, "*", "=") || CheckDouble(contents, x, "/", "=") || CheckDouble(contents, x, "%", "=") || CheckDouble(contents, x, "^", "=") || CheckDouble(contents, x, "@", "=") || CheckDouble(contents, x, "=", "=") || CheckDouble(contents, x, ">", "=") || CheckDouble(contents, x, "<", "=") || CheckDouble(contents, x, "!", "=") || CheckDouble(contents, x, "|", "|") || CheckDouble(contents, x, "&", "&") || CheckDouble(contents, x, "^", "^"))
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                data.Add(contents[x].ToString() + contents[x + 1].ToString());
                                x++;
                            }
                            else if (contents[x].ToString() == "+" || contents[x].ToString() == "-" || contents[x].ToString() == "/" || contents[x].ToString() == "*" || contents[x].ToString() == "^" || contents[x].ToString() == "@" || contents[x].ToString() == "=")
                            {
                                if (tok != "")
                                {
                                    data.Add(tok);
                                    tok = "";
                                }
                                data.Add(contents[x].ToString());
                            }
                            else
                            {
                                tok += contents[x].ToString();
                            }
                        }
                        else
                        {
                            if (contents[x].ToString() == "\"" && contents[x-1].ToString() != "\\")
                            {
                                inString = false;
                                data.Add("\"" + tok + "\"");
                                tok = "";
                            }
                            else
                            {
                                tok += contents[x].ToString();
                            }
                        }
                    }
                    else if (CheckDouble(contents, x, "*", "/"))
                    {
                        inMultiLineComment = false;
                        x++;
                    }
                }
                else if (contents[x].ToString() == "\n")
                {
                    inSingleLineComment = false;
                }
            }

            return data.ToArray();
        }

        private static bool CheckDouble(string contents, int x, string da, string db)
        {
            return (x < contents.Length - 1 && contents[x].ToString() == da && contents[x + 1].ToString() == db);
        }
    }
}
