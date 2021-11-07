using Compilable;
using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Extensions;
using Compilable.Factories;
using System;
using System.Collections.Generic;

namespace CompilableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ISwitchCaseFactory factory = new SwitchCaseFactory();
            factory.AddSwitchCase<int, string>(c =>
            {
                c.AddCase(0, "zero");
                c.AddCase(1, "one");
                c.SetDefault("default");
                return c;
            }, "getString");

            factory.TryGetProvider(
                "getString", 
                out ISwitchCaseProvider<int, string> switchCase);

            factory.TryGetDelegate(
                "getString", 
                out TryGetDelegate<int, string> _delegate);

            _delegate(1, out string one);
            _delegate(int.MinValue, out string minValue);

            Console.WriteLine(one);
            Console.WriteLine(minValue);

            IEnumerable<KeyValuePair<int, string>> cases = switchCase.AsEnumerable();

            foreach (var _case in cases)
                Console.WriteLine($"{_case.Key}:{_case.Value}");

            string _default = switchCase.GetDefaultCase();

            Console.WriteLine($"default:{_default ?? "null"}");
        }
    }
}
