using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class ArgumentSymbol : SymbolBase
    {
        public ArgumentSymbol(string emit)
        {
            Data = emit;
        }

        public override string Emit()
        {
            string data = Data as string;

            if (string.IsNullOrEmpty(data))
                return string.Empty;

            if (data[0] == '_')     // optional argument
                return $@"[{data.Substring(1)}]";
            else
                return $@"{{{data.Substring(1)}}}";
        }
    }
}