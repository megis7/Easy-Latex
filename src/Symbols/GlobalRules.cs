using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    /// <summary>
    /// Represents the global rules that are always FIRSTLY checked.
    /// </summary>
    public static class GlobalRules
    {
        public static List<RuleSet> Rules => new List<RuleSet>()
        {
            new RuleSet()
            {
                MatchRule = (c, str) => str == "env",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new EnvironmentSymbol()); }
            },

            new RuleSet()
            {
                MatchRule = (c, str) => str == "start" && c is EnvironmentSymbol,
                ActRule = (ref SymbolBase c, string str) => { (c as EnvironmentSymbol).ReceiveArguments = false; }
            },

            // zero out context
            new RuleSet()
            {
                MatchRule = (c, str) => str == "quit",
                ActRule = (ref SymbolBase c, string str) => { c = null; }
            },

            new RuleSet()
            {
                MatchRule = (c, str) => str == "escape",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new EscapeSymbol()); }
            },

            // go back d times in the tree
            new RuleSet()
            {
                MatchRule = (c, str) => str.StartsWith("end"),
                ActRule = (ref SymbolBase c, string str) =>
                {
                    int count = str.Count(ch => ch == 'd');
                    for (int i = 0; i < count; i++)
                    {
                        if (c == null)
                            break;

                        if (c.GetType() == typeof(EmptySymbol))
                            i--;

                        c = c.Parent;
                    }
                }
            }
        };
    }
}