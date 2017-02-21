using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class DirectiveSymbol : SymbolBase
    {
        public DirectiveSymbol(string name)
        {
            Data = name;
        }

        public override string Emit()
        {
            string result = string.Empty;
            Children.ForEach(c => result += c.Emit());

            return $@"\{Data}{{{result}}}";
        }
    }
}