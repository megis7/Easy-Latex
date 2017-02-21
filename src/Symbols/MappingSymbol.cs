using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class MappingSymbol : SymbolBase
    {
        public MappingSymbol(string type)
        {
            string value;
            MapTable.TryGetValue(type, out value);

            Data = value;
        }

        public static Dictionary<string, string> MapTable = new Dictionary<string, string>()
        {
            { "sum", @"\sum" },
            { "integral", @"\int"},
            { "infinity", @"\infty" },
            { "equals", @"=" },
            { "plus", @"+" },
            { "minus", @"-" },
            { "dots", @"\ldots" },
            { "cos", @"\cos" },
            { "sin", @"\sin" },
            { "log", @"\log" },
            { "then", @"\rightarrow" },
            { "mark", @"&" },
        };

        public override string Emit()
        {
            if (Data == null)
                return null;

            string result = string.Empty;
            Children.ForEach(c => result += c.Emit());

            return $@"{Data} {result}";
        }
    }
}