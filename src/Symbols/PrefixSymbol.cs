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

            string args = string.Empty;
            Arguments.ForEach(a => args += a.Emit());

            return $@"{Data}{args}{{{result}}}";
        }

        public override void ExitContext()
        {
            int index = Children.FindIndex(0, symbol => symbol.Data?.ToString() == ".");
            if (index == -1)
                return;

            Arguments = Children.Take(index).Select(c => new ArgumentSymbol(c.Data as string)).ToList();
            Children = Children.Skip(index + 1).ToList();
        }

        public List<ArgumentSymbol> Arguments { get; set; } = new List<ArgumentSymbol>();

        public static Dictionary<string, string> Symbols = new Dictionary<string, string>()
        {
            { "exponent", @"^" },
            { "expo", @"^" },
            { "sub", @"_" },
            { "sqrt", @"\sqrt" },
            { "text", @"\text" },
            { "mean", @"\overline" },
        };
    }
}