using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class EscapeSymbol : SymbolBase
    {
        public EscapeSymbol(bool escapeBefore = false)
        {
            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => true,
                ActRule = (ref SymbolBase c, string str) =>
                {
                    c.Parent.Children.Remove(this);
                    while (c != null && c.Alias != str)
                    {
                        c.ExitContext();
                        c = c?.Parent;
                    }

                    if (escapeBefore)
                    {
                        c = c?.Parent;
                        c?.ExitContext();
                    }

                    if (c == null)
                        Console.WriteLine($"Warning: escape {str} not found");
                }
            });

            EscapeBefore = escapeBefore;
        }

        // escapes before the given context else escapes at the given context.
        public bool EscapeBefore { get; set; } = false;

        public override string Emit()
        {
            throw new NotImplementedException();
        }
    }
}