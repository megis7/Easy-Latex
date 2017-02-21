using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class PrefixSymbol : SymbolBase
    {
        public PrefixSymbol(string emit)
        {
            Data = emit;
        }

        public override string Emit()
        {
            string result = string.Empty;
            Children.ForEach(c => result += c.Emit());

            return $@"{Data}{{{result}}}";
        }

        public static Dictionary<string, string> Symbols = new Dictionary<string, string>()
        {
            { "exponent", @"^" },
            { "sub", @"_" },
            { "sqrt", @"\sqrt" },
            { "text", @"\text" },
        };
    }
}