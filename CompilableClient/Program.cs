using Compilable;
using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Extensions;
using Compilable.Factories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CompilableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new SwitchCaseBuilder<int, string>();
            var range = Enumerable.Range(0, 1000);

            foreach (var item in range)
                builder.AddCase(item, () => item.ToString());

            Console.WriteLine("Builder configured");
            var provider = builder.GetSwitchCase();
            var tryGetDigit = provider.GetDelegate();
            Console.WriteLine("SwitchCase compiled");
            var threads = new Thread[range.Count()];

            var falseResults = new ConcurrentDictionary<int, string>();
            var start = false;
            var counter = 0;
            foreach (var item in range)
            {
                threads[counter++] = new Thread(() =>
                {
                    while (!start)
                        Thread.Sleep(0);

                    tryGetDigit(item, out string value);

                    if (item.ToString() != value)
                        falseResults.AddOrUpdate(item, value, (i, v) => v);

                    lock (builder)
                        counter--;
                });
                threads[counter - 1].Start();
            }

            start = true;
            Console.WriteLine("Threads started");
            while (counter != 0)
                Thread.Sleep(0);

            foreach (var item in falseResults)
                Console.WriteLine(item.Key + ":" + item.Value);
        }
    }
}
