using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFE
{

    struct ExpressionData
    {
        public int curExpression;
        public Expression exp;
        public int x;
    }

    class Statement
    {
        int id;
        public List<Expression> Expressions;
        public Statement(int id)
        {
            this.id = id;
            Expressions = new List<Expression>();
        }

        public override string ToString()
        {
            string returnString = "";
            returnString += "{Statement " + id.ToString() + ": ";
            if (Expressions.Count > 0)
            {
                returnString += "{Expressions :";
                for (int x = 0; x < Expressions.Count; x++)
                {
                    returnString += Expressions[x].ToString();
                    if (x < Expressions.Count - 1)
                    {
                        returnString += ",";
                    }
                }
                returnString += "}";
            }
            else
            {
                returnString += "NULL";
            }
            returnString += "}";
            return returnString;
        }
    }

    class Expression
    {
        int id;
        public List<List<Expression>> Arguments;
        public List<List<Expression>> Indices;
        public List<Statement> Nested;
        public Directive DIR;
        byte[] data;
        public Expression(int id, Directive DIR, byte[] data)
        {
            this.id = id;
            this.DIR = DIR;
            this.data = data;
            Arguments = new List<List<Expression>>();
            Indices = new List<List<Expression>>();
            Nested = new List<Statement>();
        }

        public override string ToString()
        {
            string returnString = "";
            returnString += "<Expression " + id.ToString() + ": ";
            returnString += "DIR = " + DIR.ToString() + "; ";
            returnString += "DATA = " + data.ToString() + "; ";
            if (Arguments.Count > 0)
            {
                returnString += "<Arguments: ";
                for (int x = 0; x < Arguments.Count; x ++)
                {
                    returnString += "<Argument " + x.ToString() + ": ";
                    for (int y = 0; y < Arguments[x].Count; y ++)
                    {
                        returnString += Arguments[x][y].ToString();
                        if (y < Arguments[x].Count - 1)
                        {
                            returnString += ",";
                        }
                    }
                    returnString += ">";
                    if (x < Arguments.Count - 1)
                    {
                        returnString += ",";
                    }
                }
                returnString += ">";
            }
            if (Indices.Count > 0)
            {
                returnString += "<Indices: ";
                for (int x = 0; x < Indices.Count; x++)
                {
                    returnString += "<Index " + x.ToString() + ": ";
                    for (int y = 0; y < Indices[x].Count; y++)
                    {
                        returnString += Indices[x][y].ToString();
                        if (y < Indices[x].Count - 1)
                        {
                            returnString += ",";
                        }
                    }
                    returnString += ">";
                    if (x < Indices.Count - 1)
                    {
                        returnString += ",";
                    }
                }
            }
            if (Nested.Count > 0)
            {
                returnString += "<Nested: ";
                for (int x = 0; x < Nested.Count; x++)
                {
                    returnString += Nested[x].ToString();
                    if (x < Nested.Count - 1)
                    {
                        returnString += ",";
                    }
                }
                returnString += ">";
            }
            return returnString;
        }
    }

    class Scope
    {
        int id;
        List<Variable> Variables;
        List<Function> Functions;
    }

    class Variable
    {
        int id;
        string varname;
        DataType DT;
        byte[] data;
    }

    class Function
    {
        int id;
        string funcname;
        DataType RT;
        List<Variable> Arguments;
        List<Statement> Contents;
    }

    class Memory
    {
        List<Scope> Scopes;
    }

    public enum DataType
    {
        DTdec,
        DTint,
        DTsho,
        DTlon,
        DTdob,
        DTflo,
        DTbol,
        DTstr,
        DTfun,
        DTref,
        DTvoi
    }

    public enum Directive
    {
        Ddec,
        Dint,
        Dsho,
        Dlon,
        Ddob,
        Dflo,
        Dbol,
        Dstr,
        Dfun,
        Dref,
        Dvoi,

        Dadd,
        Dsub,
        Dmul,
        Ddiv,
        Dpow,
        Drot,
        Dmod,

        Dasn,

        Deq,
        Dneq,
        Dlteq,
        Dgteq,
        Dlt,
        Dgt,

        Dand,
        Dor,
        Dxor,

        Dif,
        Delse,
        Delif,
        
        Dwhile,
        Ddowhile,
        Dfor,
        Dforeach,

        Dendl,

        Dcol,
        Dswitch,
        Dbreak,

        Dreturn,
        Dfunc,
        Ddecl,

        Dvar,
    }
}
