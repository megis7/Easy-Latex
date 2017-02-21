using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLatex.Symbols
{
    public class RuleSet
    {
        public delegate bool Match(SymbolBase context, string word);

        public delegate void Act(ref SymbolBase context, string word);

        public Match MatchRule { get; set; }
        public Act ActRule { get; set; }
    }

    public abstract class SymbolBase
    {
        public SymbolBase()
        {
            Description = GetType().ToString().Split('.').Last();
        }

        public SymbolBase Parent { get; set; }

        public List<SymbolBase> Children { get; private set; } = new List<SymbolBase>();
        public object Data { get; set; }
        public string Description { get; set; }

        public string Alias { get; set; }

        public List<RuleSet> Rules { get; private set; } = new List<RuleSet>();

        public virtual SymbolBase AddChild(SymbolBase symbol)
        {
            symbol.Parent = this;
            Children.Add(symbol);

            return symbol;
        }

        public abstract string Emit();

        public virtual string DebugEmit(int level)
        {
            if (Children.Count == 0)
                return new string('-', level) + Description + " " + Data + "\n";

            string children = new string('-', level) + Description + " " + Data + "\n";
            Children.ForEach(c => children += c.DebugEmit(level + 1));

            return children;
        }

        public override string ToString()
        {
            return Emit();
        }
    }
}