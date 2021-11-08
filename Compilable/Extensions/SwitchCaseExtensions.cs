using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Extensions
{
    public static class SwitchCaseExtensions
    {
        public static IEnumerable<KeyValuePair<TCase, TValue>> AsEnumerable<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var cases = switchExpression.Cases;
            return cases
                .Select(c => new KeyValuePair<TCase, TValue>(
                    (TCase)((ConstantExpression)c.TestValues[0]).Value,
                    (TValue)((ConstantExpression)((BinaryExpression)((BlockExpression)c.Body).Expressions[0]).Right).Value));
        }
        public static TValue GetDefaultCase<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            return (TValue)((ConstantExpression)((BinaryExpression)((BlockExpression)switchExpression.DefaultBody).Expressions[0]).Right).Value;
        }
        public static ISwitchCaseBuilder<TCase, TValue> ToSwitchCaseBuilder<TCase, TValue, T>(this IEnumerable<T> enumerable, Func<T, TCase> caseSelector, Func<T, TValue> valueSelector)
        {
            var builder = new SwitchCaseBuilder<TCase, TValue>();

            foreach (var item in enumerable)
                builder.AddSingletonCase(caseSelector(item), valueSelector(item));

            return builder;
        }
    }
}
