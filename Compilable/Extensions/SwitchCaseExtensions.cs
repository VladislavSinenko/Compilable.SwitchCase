using Compilable.Adapters;
using Compilable.Builders;
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
        private static ISwitchCaseProvider<ExpressionType, IGetExpressionValue> provider;

        static SwitchCaseExtensions()
        {
            ISwitchCaseBuilder<ExpressionType, IGetExpressionValue> builder = new SwitchCaseBuilder<ExpressionType, IGetExpressionValue>();
            builder.AddCase(ExpressionType.Constant, () => new GetConstantValueStrategy());
            builder.AddCase(ExpressionType.Call, () => new GetExpressionCallStrategy());
            provider = builder.GetSwitchCase();
        }
        public static IEnumerable<KeyValuePair<TCase, TValue>> AsEnumerable<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var cases = switchExpression.Cases;

            foreach (var item in cases)
                yield return new KeyValuePair<TCase, TValue>(KeySelector(item), ValueSelector(item));

            TCase KeySelector(SwitchCase item)
            {
                var testValue = (ConstantExpression)item.TestValues[0];
                return (TCase)testValue.Value;
            }
            TValue ValueSelector(SwitchCase item)
            {
                var adapter = new ExpressionValueAdapter(provider);
                var itemBody = (BlockExpression)item.Body;
                var assignment = (BinaryExpression)itemBody.Expressions[0];
                var value = adapter.GetValue<TValue>(assignment.Right);
                return value;
            }
        }
        public static TValue GetDefaultCase<TCase, TValue>(this ISwitchCaseProvider<TCase, TValue> switchCase)
        {
            var expression = switchCase.GetExpression();
            var body = (BlockExpression)expression.Body;
            var switchExpression = (SwitchExpression)body.Expressions[0];
            var adapter = new ExpressionValueAdapter(provider);
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
