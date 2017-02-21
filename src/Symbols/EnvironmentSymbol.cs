using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class EnvironmentSymbol : SymbolBase
    {
        public EnvironmentSymbol()
        {
            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => Environments.Contains(str),
                ActRule = (ref SymbolBase c, string str) => { Data = str; Alias = str; }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, str) => ReceiveArguments,
                ActRule = (ref SymbolBase c, string str) => Arguments.Add(str)
            });
        }

        public static List<string> Environments = new List<string>()
        {
            "cases",
            "align",
            "align*",
            "aligned",
            "enumerate",
            "equation",
            "equation*",
            "alignat",
            "alignedat",
            "itemize"
        };

        public List<string> Arguments { get; set; } = new List<string>();

        public bool ReceiveArguments { get; set; } = true;

        public override string Emit()
        {
            string result = string.Empty;
            Children.ForEach(c => result += c.Emit());

            string arguments = string.Empty;
            Arguments.ForEach(arg => arguments += $@"{{{arg}}}");

            return $@"\begin{{{Data}}}{arguments}{result}\end{{{Data}}}";
        }
    }
}