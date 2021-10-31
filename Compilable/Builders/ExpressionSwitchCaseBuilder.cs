using Compilable.Delegates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Builders
{
    public class ExpressionSwitchCaseBuilder<TKey, TValue> : ISwitchCaseBuilder<TKey, TValue>, IExpressionSwitchCaseBuilder<TKey, TValue>
    {
        private ParameterExpression _caseValue = Expression.Parameter(typeof(TKey), nameof(_caseValue));
        private ParameterExpression _outValue = Expression.Parameter(typeof(TValue).MakeByRefType(), nameof(_outValue));
        private ConstantExpression returnTrue = Expression.Constant(true, typeof(bool));
        private Queue<SwitchCase> _cases = new Queue<SwitchCase>();
        private BlockExpression _defaultCase = null;
        public ISwitchCaseBuilder<TKey, TValue> AddCase(TKey _case, TKey value)
        {
            ConstantExpression valueExpression = Expression.Constant(value, typeof(TValue));
            BinaryExpression asignOutVar = Expression.Assign(_outValue, valueExpression);
            BlockExpression caseBlock = Expression.Block(asignOutVar, returnTrue);
            ConstantExpression testValue = Expression.Constant(_case, typeof(TKey));
            SwitchCase switchCase = Expression.SwitchCase(caseBlock, testValue);
            _cases.Enqueue(switchCase);
            return this;
        }
        public ISwitchCase<TKey, TValue> GetSwitchCase()
        {
            _defaultCase = _defaultCase ?? GetDefaultBlock(default(TValue));
            SwitchExpression switchExpression = Expression.Switch(_caseValue, _defaultCase, _cases.ToArray());
            BlockExpression blockExpression = Expression.Block(new[] { _caseValue, _outValue }, switchExpression);
            Expression<TryGetDelegate<TKey, TValue>> lambdaExpression = Expression.Lambda<TryGetDelegate<TKey, TValue>>(blockExpression);
            return new ExpressionSwitchCase<TKey, TValue>(lambdaExpression);
        }
        public IExpressionSwitchCase<TKey, TValue> GetExpressionSwitchCase()
        {
            return (IExpressionSwitchCase<TKey, TValue>)GetSwitchCase();
        }
        public ISwitchCaseBuilder<TKey, TValue> SetDefault(TValue value)
        {
            _defaultCase = GetDefaultBlock(value);
            return this;
        }
        private BlockExpression GetDefaultBlock(TValue value)
        {
            return Expression.Block(Expression.Assign(_outValue, Expression.Constant(value, typeof(TValue))));
        }
    }
}
