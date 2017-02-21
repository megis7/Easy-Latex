using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLatex.Symbols;
using System.IO;
using System.Diagnostics;

namespace EasyLatex
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Not enough arguments provided.");
                Console.ReadKey();
                return;
            }

            string[] lines = File.ReadAllLines(args[0]);

            string expression = lines.Aggregate((a, b) => a + " " + b);
            while (expression.Contains("  "))
                expression = expression.Replace("  ", " "); // kill double spaces

            List<SymbolBase> Symbols = new List<SymbolBase>();
            SymbolBase activeSymbol = new EmptySymbol();
            SymbolBase context = activeSymbol;

            foreach (string word in expression.Split(' '))
            {
                if (context == null)
                {
                    Symbols.Add(activeSymbol);
                    activeSymbol = new EmptySymbol();
                    context = activeSymbol;
                }

                SymbolFactory.Process(ref context, word);
            }

            Symbols.Add(activeSymbol);
            string result = string.Empty;

            foreach (SymbolBase s in Symbols)
                result += s.Emit() + "\n";

            string debug = string.Empty;
            foreach (SymbolBase s in Symbols)
                debug += s.DebugEmit(0) + "\n";

            Console.WriteLine(debug);

            // hardcode some directives and environments... can be removed now by using env and escape keywords.
            result = $@"\documentclass[10pt, fleqn]{{article}}\n\usepackage{{amsmath}}\n\usepackage{{fullpage}}\n\begin{{document}}\n{result}\n\end{{document}}";

            string[] output = result.Split(new string[] { "\\n" }, StringSplitOptions.RemoveEmptyEntries);

            File.WriteAllLines("latex.tex", output);
        }
    }
}