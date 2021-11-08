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
            factory.AddSwitchCase<int, object>(c =>
            {
                c.AddCase(0, () => new object());
                c.AddSingletonCase(1, new object());
                c.SetDefaultAsSingleton("default");
                return c;
            }, "getString");

            factory.TryGetProvider(
                "getString", 
                out ISwitchCaseProvider<int, object> switchCase);

            factory.TryGetDelegate(
                "getString", 
                out TryGetDelegate<int, object> _delegate);

            _delegate(1, out object one);
            _delegate(int.MinValue, out object minValue);

            Console.WriteLine(one);
            Console.WriteLine(minValue);

            //IEnumerable<KeyValuePair<int, object>> cases = switchCase.AsEnumerable();

            //foreach (var _case in cases)
            //    Console.WriteLine($"{_case.Key}:{_case.Value}");

            string _default = (string)switchCase.GetDefaultCase();

            Console.WriteLine($"default:{_default ?? "null"}");

            _delegate(0, out var zero1);
            _delegate(0, out var zero2);

            _delegate(1, out var one2);

            Console.WriteLine(zero1.Equals(zero2));
            Console.WriteLine(one.Equals(one2));
        }
    }
}
