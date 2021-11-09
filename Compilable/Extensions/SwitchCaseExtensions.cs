using Compilable.Adapters;
using Compilable.Builders;
using Compilable.Containers;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Extensions
{
    
    public static class SwitchCaseExtensions
    {
        public static IEnumerable<TCase> GetCases<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var cases = switchExpression.Cases;

            foreach (var item in cases)
                yield return (TCase)((ConstantExpression)item.TestValues[0]).Value;
        }
        public static IEnumerable<TValue> GetValues<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var cases = GetCases(switchCase);
            var tryGetValue = switchCase.GetDelegate();
            foreach (var item in cases)
            {
                tryGetValue(item, out TValue value);
                yield return value;
            }
        }
        public static IEnumerable<KeyValuePair<TCase, TValue>> AsEnumerable<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var cases = GetCases(switchCase);
            var tryGetValue = switchCase.GetDelegate();
            foreach (var item in cases)
            {
                tryGetValue(item, out TValue value);
                yield return new KeyValuePair<TCase, TValue>(item, value);
            }
        }
        public static TValue GetDefaultCase<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var adapter = new ExpressionValueAdapter(SwitchCaseContainer.GetExpressionProvider);
            var defBody = (BlockExpression)switchExpression.DefaultBody;
            var asigment = (BinaryExpression)defBody.Expressions[0];
            var value = adapter.GetValue<TValue>(asigment.Right);
            return value;
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
