using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class RawSymbol : SymbolBase
    {
        public RawSymbol(string s)
        {
            Data = s;
        }

        public override string Emit()
        {
            string result = $@"{Data.ToString()} ";
            foreach (SymbolBase symbol in Children)
                result += symbol.Emit();

            return result;
        }
    }
}