using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public static class SymbolFactory
    {
        public static void Process(ref SymbolBase context, string word)
        {
            RuleSet rule = GetMatchingRuleSet(context, word);

            // Do default action => treat word as terminal raw symbol
            if (rule == null)
                context.AddChild(new RawSymbol(word));
            else
                rule.ActRule(ref context, word);
        }

        private static RuleSet GetMatchingRuleSet(SymbolBase activeContext, string word)
        {
            foreach (RuleSet rule in GlobalRules.Rules)
                if (rule.MatchRule(activeContext, word))
                    return rule;

            return GetMatchingRuleSetRecursive(activeContext, activeContext, word);
        }

        private static RuleSet GetMatchingRuleSetRecursive(SymbolBase context, SymbolBase activeContext, string word)
        {
            if (context == null)
                return null;

            foreach (RuleSet rule in context.Rules)
                if (rule.MatchRule(activeContext, word))
                    return rule;

            return GetMatchingRuleSetRecursive(context.Parent, activeContext, word);
        }
    }
}