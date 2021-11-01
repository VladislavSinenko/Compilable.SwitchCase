using Compilable;
using Compilable.Builders;
using Compilable.Extensions;
using System;

namespace CompilableClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ExpressionSwitchCaseBuilder<int, string>();
            builder.AddCase(0, "zero");
            builder.AddCase(1, "one");

            IExpressionSwitchCase<int, string> switchCase = builder.GetExpressionSwitchCase();

            Console.WriteLine(switchCase.TryGetCase(1, out string one) ? one : "null");
            Console.WriteLine(switchCase.TryGetCase(int.MinValue, out string minValue) ? minValue : "null");

            var cases = switchCase.AsEnumerable();
            foreach (var _case in cases)
                Console.WriteLine($"{_case.Key}:{_case.Value}");

            var _default = switchCase.GetDefaultCase();

            Console.WriteLine($"default:{_default ?? "null"}");
        }
    }
}
