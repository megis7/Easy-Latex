using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class FenceSymbol : SymbolBase
    {
        public FenceSymbol()
        {
            Rules.Add(new RuleSet()
            {
                MatchRule = (c, word) => word == "bracket",
                ActRule = (ref SymbolBase c, string word) => { FenceBegin = "["; FenceEnd = "]"; }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, word) => word == "bar",
                ActRule = (ref SymbolBase c, string word) => { FenceBegin = "|"; FenceEnd = "|"; }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, word) => word == "brace",
                ActRule = (ref SymbolBase c, string word) => { FenceBegin = @"\{"; FenceEnd = @"\}"; }
            });

            Rules.Add(new RuleSet()
            {
                MatchRule = (c, word) => word == "angle",
                ActRule = (ref SymbolBase c, string word) => { FenceBegin = @"<"; FenceEnd = @">"; }
            });
        }

        public string FenceBegin { get; set; } = "(";
        public string FenceEnd { get; set; } = ")";

        public bool LeftHug { get; set; } = true;
        public bool RightHug { get; set; } = true;

        public bool UseLeftRight { get; set; } = true;

        public override string Emit()
        {
            string result = string.Empty;

            Children.ForEach(c => result += c.Emit());

            if (!UseLeftRight)
                return $@"{(LeftHug ? FenceBegin : "")}{result}{(RightHug ? FenceEnd : "")}";
            else
                return $@"\left{(LeftHug ? FenceBegin : ".")}{result}\right{(RightHug ? FenceEnd : ".")}";
        }
    }
}