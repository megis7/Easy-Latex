using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class EscapeSymbol : SymbolBase
    {
        public EscapeSymbol()
        {
            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => true,
                ActRule = (ref SymbolBase c, string str) =>
                {
                    c.Parent.Children.Remove(this);     // remove me from the hierarchy.
                    while (c != null && c.Alias != str)
                        c = c?.Parent;

                    if (c == null)
                        Console.WriteLine($"Warning: escape {str} not found");
                }
            });
        }

        public override string Emit()
        {
            throw new NotImplementedException();
        }
    }
}