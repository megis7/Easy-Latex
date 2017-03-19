using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class EmptySymbol : SymbolBase
    {
        public EmptySymbol()
        {
            Description = "Empty Symbol";

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => PrefixSymbol.Symbols.ContainsKey(str),
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new PrefixSymbol(PrefixSymbol.Symbols[str]) { Alias = str }); }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "fence",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new FenceSymbol() { Alias = str }); }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "math",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new FenceSymbol() { FenceBegin = "$", FenceEnd = "$", UseLeftRight = false, Alias = "math" }); }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "lhug",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new FenceSymbol() { Alias = str, RightHug = false }); }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "rhug",
                ActRule = (ref SymbolBase c, string str) => { c = c.AddChild(new FenceSymbol() { Alias = str, LeftHug = false }); }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => str == "fraction" || str == "frac",
                ActRule = (ref SymbolBase c, string str) =>
                {
                    FractionSymbol frac = new FractionSymbol() { Alias = str };
                    frac.AddChild(new EmptySymbol());

                    c.AddChild(frac);
                    c = frac.Children.Last();
                }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => MappingSymbol.MapTable.ContainsKey(str) == true,
                ActRule = (ref SymbolBase c, string str) => { c.AddChild(new MappingSymbol(str)); }
            });
        }

        public override string Emit()
        {
            string result = string.Empty;
            Children.ForEach(c => result += c.Emit());

            return result;
        }
    }
}