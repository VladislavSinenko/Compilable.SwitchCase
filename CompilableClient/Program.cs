using Compilable;
using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Extensions;
using Compilable.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompilableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new SwitchCaseBuilder<int, string>();
            var range = Enumerable.Range(0, 100000);

            foreach (var item in range)
                builder.AddSingletonCase(item, item.ToString());

            var provider = builder.GetSwitchCase();
            var tryGetDigit = provider.GetDelegate();

            var tuples = range.AsParallel().Select(n => { tryGetDigit(n, out string value); return n.ToString() != value; });

            var errors = tuples.Where(t => t).Count();
            Console.WriteLine(errors);
        }
    }
}
