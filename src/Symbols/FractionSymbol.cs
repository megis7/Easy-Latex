using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class FractionSymbol : SymbolBase
    {
        public FractionSymbol()
        {
            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "over",
                ActRule = (ref SymbolBase c, string str) => { AddChild(new EmptySymbol()); c = Children.Last(); }
            });
        }

        public override string Emit()
        {
            string result = string.Empty;

            var child1 = Children.ElementAtOrDefault(0);
            var child2 = Children.ElementAtOrDefault(1);

            return $@"\frac{{{child1?.Emit()}}}{{{child2?.Emit()}}}";
        }
    }
}