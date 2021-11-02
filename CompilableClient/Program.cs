using Compilable;
using Compilable.Builders;
using Compilable.Extensions;
using System;
using System.Collections.Generic;

namespace CompilableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ISwitchCaseBuilder<int, string> builder = new SwitchCaseBuilder<int, string>();
            builder.AddCase(0, "zero");
            builder.AddCase(1, "one");
            builder.SetDefault("min value");

            ISwitchCase<int, string> switchCase = builder.GetSwitchCase();

            switchCase.TryGetCase(1, out string one);
            switchCase.TryGetCase(int.MinValue, out string minValue);

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
