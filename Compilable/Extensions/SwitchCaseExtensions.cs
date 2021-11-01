using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Extensions
{
    public static class SwitchCaseExtensions
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> AsEnumerable<TKey, TValue>(this IExpressionSwitchCase<TKey, TValue> switchCase)
        {
            var expression = (LambdaExpression)switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var cases = switchExpression.Cases;
            return cases
                .Select(c => new KeyValuePair<TKey, TValue>(
                    (TKey)((ConstantExpression)c.TestValues[0]).Value,
                    (TValue)((ConstantExpression)((BinaryExpression)((BlockExpression)c.Body).Expressions[0]).Right).Value));
        }
        public static TValue GetDefaultCase<TKey, TValue>(this IExpressionSwitchCase<TKey, TValue> switchCase)
        {
            var expression = (LambdaExpression)switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            return (TValue)((ConstantExpression)((BinaryExpression)((BlockExpression)switchExpression.DefaultBody).Expressions[0]).Right).Value;
        }
    }
}
