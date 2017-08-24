using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{
    class Lexer
    {
         public static List<Statement> Lex(string[] data)
        {
            int curStatement = 0;
            int curExpression = 0;
            List<Statement> stats = new List<Statement>();
            stats.Add(new Statement(curStatement));
            curStatement++;
            for (int x = 0; x < data.Length; x++)
            {
                ExpressionData expdat = getExpression(data, x, curExpression);
                curExpression = expdat.curExpression;
                x = expdat.x;
                stats[curStatement - 1].Expressions.Add(expdat.exp);
                if (x < data.Length - 1 && stats[curStatement - 1].Expressions[stats[curStatement - 1].Expressions.Count - 1].DIR == Directive.Dendl || stats[curStatement - 1].Expressions[stats[curStatement - 1].Expressions.Count - 1].Nested.Count > 0)
                {
                    stats.Add(new Statement(curStatement));
                    curStatement++;
                }
            }
            return stats;
        }

        private static ExpressionData getExpression(string[] data, int x, int curExpression)
        {
            ExpressionData expdat = new ExpressionData();
            expdat.curExpression = curExpression;
            expdat.x = x;
            expdat.exp = new Expression(-4, Directive.Dneq, new Byte[0]);
            switch (data[x])
            {
                case "+":
                    expdat.exp = (new Expression(curExpression, Directive.Dadd, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "-":
                    expdat.exp = (new Expression(curExpression, Directive.Dsub, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "*":
                    expdat.exp = new Expression(curExpression, Directive.Dmul, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "/":
                    expdat.exp = new Expression(curExpression, Directive.Ddiv, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "^":
                    expdat.exp = new Expression(curExpression, Directive.Dpow, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "@":
                    expdat.exp = new Expression(curExpression, Directive.Drot, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "%":
                    expdat.exp = new Expression(curExpression, Directive.Dmod, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "=":
                    expdat.exp = new Expression(curExpression, Directive.Dasn, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "==":
                    expdat.exp = new Expression(curExpression, Directive.Deq, new Byte[0]);
                    expdat.curExpression++;
                    break;
                case "!=":
                    expdat.exp = (new Expression(curExpression, Directive.Dneq, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "<=":
                    expdat.exp = (new Expression(curExpression, Directive.Dlteq, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case ">=":
                    expdat.exp = (new Expression(curExpression, Directive.Dgteq, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "<":
                    expdat.exp = (new Expression(curExpression, Directive.Dlt, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case ">":
                    expdat.exp = (new Expression(curExpression, Directive.Dgt, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "&&":
                    expdat.exp = (new Expression(curExpression, Directive.Dand, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "||":
                    expdat.exp = (new Expression(curExpression, Directive.Dor, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "^^":
                    expdat.exp = (new Expression(curExpression, Directive.Dxor, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "if":
                    expdat.exp = (new Expression(curExpression, Directive.Dif, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "else":
                    expdat.exp = (new Expression(curExpression, Directive.Delse, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "elif":
                    expdat.exp = (new Expression(curExpression, Directive.Delif, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "while":
                    expdat.exp = (new Expression(curExpression, Directive.Dwhile, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "dowhile":
                    expdat.exp = (new Expression(curExpression, Directive.Ddowhile, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "for":
                    expdat.exp = (new Expression(curExpression, Directive.Dfor, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "foreach":
                    expdat.exp = (new Expression(curExpression, Directive.Dforeach, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case ";":
                    expdat.exp = (new Expression(curExpression, Directive.Dendl, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case ":":
                    expdat.exp = (new Expression(curExpression, Directive.Dcol, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "switch":
                    expdat.exp = (new Expression(curExpression, Directive.Dswitch, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "break":
                    expdat.exp = (new Expression(curExpression, Directive.Dbreak, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "return":
                    expdat.exp = (new Expression(curExpression, Directive.Dreturn, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "func":
                    expdat.exp = (new Expression(curExpression, Directive.Dfunc, new Byte[0]));
                    expdat.curExpression++;
                    break;
                case "true":
                    expdat.exp = (new Expression(curExpression, Directive.Dbol, BitConverter.GetBytes(true)));
                    expdat.curExpression++;
                    break;
                case "false":
                    expdat.exp = (new Expression(curExpression, Directive.Dbol, BitConverter.GetBytes(false)));
                    expdat.curExpression++;
                    break;
                case "void":
                    expdat.exp = (new Expression(curExpression, Directive.Dvoi, new Byte[0]));
                    expdat.curExpression++;
                    break;
                default:
                    if (data[x][0].ToString() == "\"" && data[x][data[x].Length - 1].ToString() == "\"")
                    {
                        expdat.exp = (new Expression(curExpression, Directive.Dbol, Encoding.ASCII.GetBytes(string.Join("", data[x].Skip(1).Take(data[x].Length - 2).ToArray()))));
                        expdat.curExpression++;
                    }
                    else if (isNumeric(data[x]))
                    {
                        switch (determineBestNumericDataType(data[x]))
                        {
                            case DataType.DTsho:
                                expdat.exp = (new Expression(curExpression, Directive.Dsho, BitConverter.GetBytes(Convert.ToInt16(data[x]))));
                                expdat.curExpression++;
                                break;
                            case DataType.DTint:
                                expdat.exp = (new Expression(curExpression, Directive.Dint, BitConverter.GetBytes(Convert.ToInt32(data[x]))));
                                expdat.curExpression++;
                                break;
                            case DataType.DTlon:
                                expdat.exp = (new Expression(curExpression, Directive.Dlon, BitConverter.GetBytes(Convert.ToInt64(data[x]))));
                                expdat.curExpression++;
                                break;
                            case DataType.DTflo:
                                expdat.exp = (new Expression(curExpression, Directive.Dflo, BitConverter.GetBytes((float)Convert.ToDouble(data[x]))));
                                expdat.curExpression++;
                                break;
                            case DataType.DTdob:
                                expdat.exp = (new Expression(curExpression, Directive.Ddob, BitConverter.GetBytes(Convert.ToDouble(data[x]))));
                                expdat.curExpression++;
                                break;
                            case DataType.DTdec:
                                expdat.exp = (new Expression(curExpression, Directive.Dsho, IFE.BitconverterExt.GetBytes(Convert.ToDecimal(data[x]))));
                                expdat.curExpression++;
                                break;
                        }
                    }
                    else
                    {
                        expdat.exp = (new Expression(curExpression, Directive.Dvar, Encoding.ASCII.GetBytes(data[x])));
                        expdat.curExpression++;
                    }
                    break;
            }
            if (x < data.Length - 1 && data[x + 1] == "(")
            {
                int[][] positions = GetArgumentPositions(data, x);
                for (int y = 0; y < positions.Length - 1; y++)
                {
                    expdat.exp.Arguments.Add(new List<Expression>());
                    for (int z = 0; z < positions[y].Length; z++)
                    {
                        expdat.exp.Arguments[y].Add(getExpression(data, positions[y][z], expdat.curExpression).exp);
                        expdat.curExpression++;
                    }
                }
                x = positions[positions.Length - 1][positions[positions.Length - 1].Length - 1] - 1;
            }
            if (x < data.Length - 1 && data[x + 1] == "[")
            {
                int[][] positions = GetArgumentPositions(data, x);
                for (int y = 0; y < positions.Length - 1; y++)
                {
                    expdat.exp.Indices.Add(new List<Expression>());
                    for (int z = 0; z < positions[y].Length; z++)
                    {
                        expdat.exp.Indices[y].Add(getExpression(data, positions[y][z], expdat.curExpression).exp);
                        expdat.curExpression++;
                    }
                }
                x = positions[positions.Length - 1][positions[positions.Length - 1].Length - 1] - 1;
            }
            if (x < data.Length - 1 && data[x + 1] == "{")
            {
                int[][] positions = GetArgumentPositions(data, x);
                expdat.exp.Nested.AddRange(Lex(data.Skip(positions[0][0]).Take(positions[positions.Length - 1][positions[positions.Length - 1].Length - 1]).ToArray()));
                x = positions[positions.Length - 1][positions[positions.Length - 1].Length - 1] - 1;
            }
            expdat.x = x;
            return expdat;
        }

        private static bool isNumeric(string data)
        {
            for (int x = 0; x < data.Length; x++)
            {
                if (data[x] != '1' && data[x] != '2' && data[x] != '3' && data[x] != '4' && data[x] != '5' && data[x] != '6' && data[x] != '7' && data[x] != '8' && data[x] != '9' && data[x] != '0' && data[x] != '.')
                {
                    return false;
                }
            }
            return true;
        }

        private static DataType determineBestNumericDataType(string data)
        {
            bool isDec = false;
            for (int x = 0; x < data.Length; x ++)
            {
                if (data[x] == '.')
                {
                    isDec = true;
                }
            }

            if (isDec)
            {
                decimal decimalvalue = Convert.ToDecimal(data);
                decimal floatvalue = (decimal)((float)Convert.ToDouble(data));
                decimal doublevalue = (decimal)Convert.ToDouble(data);
                if (decimalvalue == floatvalue)
                {
                    return DataType.DTflo;
                }
                else if (decimalvalue == doublevalue)
                {
                    return DataType.DTdob;
                }
                else
                {
                    return DataType.DTdec;
                }
            }
            else
            {
                long longvalue = Convert.ToInt64(data);
                if (short.MinValue <= longvalue && longvalue <= short.MaxValue)
                {
                    return DataType.DTsho;
                }
                else if (int.MinValue <= longvalue && longvalue <= int.MaxValue)
                {
                    return DataType.DTint;
                }
                else
                {
                    return DataType.DTlon;
                }
            }
        }

        private static int[][] GetArgumentPositions(string[] data, int x)
        {
            List<List<int>> returnVal = new List<List<int>>();
            returnVal.Add(new List<int>());
            int curArg = 0;

            int crement = 0;
            x += 1;

            do
            {
                if (data[x] == "(" || data[x] == "{" || data[x] == "[")
                {
                    crement++;
                }
                else if (data[x] == ")" || data[x] == "}" || data[x] == "]")
                {
                    crement--;
                }
                else if (crement == 1) {
                    if (data[x] == ",")
                    {
                        curArg++;
                        returnVal.Add(new List<int>());
                    }
                    else
                    {
                        returnVal[curArg].Add(x);
                    }
                }
                x++;
            } while (crement != 0);

            curArg++;
            returnVal.Add(new List<int>());
            returnVal[curArg].Add(x);

            int[][] final = new int[returnVal.Count][]; 
            for (int z = 0; z < returnVal.Count; z++)
            {
                final[z] = new int[returnVal[z].Count];
                for (int n = 0; n < returnVal[z].Count; n++)
                {
                    final[z][n] = returnVal[z][n];
                }
            }
            return final;
         }
    }
}
