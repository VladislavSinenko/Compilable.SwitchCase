using Compilable.Delegates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Builders
{
    public class SwitchCaseBuilder<TKey, TValue> : ISwitchCaseBuilder<TKey, TValue>
    {
        private ParameterExpression _caseValue = Expression.Parameter(typeof(TKey), nameof(_caseValue));
        private ParameterExpression _outValue = Expression.Parameter(typeof(TValue).MakeByRefType(), nameof(_outValue));
        private Queue<SwitchCase> _cases = new Queue<SwitchCase>();
        private BlockExpression _defaultCase = null;
        public ISwitchCaseBuilder<TKey, TValue> AddCase(TKey _case, TValue value)
        {
            BlockExpression caseBlock = GetExpressionAssignAndReturn(_outValue, value, true);
            ConstantExpression testValue = Expression.Constant(_case, typeof(TKey));
            SwitchCase switchCase = Expression.SwitchCase(caseBlock, testValue);
            _cases.Enqueue(switchCase);
            return this;
        }
        public ISwitchCase<TKey, TValue> GetSwitchCase()
        {
            _defaultCase = _defaultCase ?? GetExpressionAssignAndReturn(_outValue, default(TValue), false);
            SwitchExpression switchExpression = Expression.Switch(_caseValue, _defaultCase, _cases.ToArray());
            BlockExpression blockExpression = Expression.Block(switchExpression);
            Expression<TryGetDelegate<TKey, TValue>> lambdaExpression = Expression.Lambda<TryGetDelegate<TKey, TValue>>(blockExpression, _caseValue, _outValue);
            return new SwitchCase<TKey, TValue>(lambdaExpression);
        }
        public ISwitchCaseBuilder<TKey, TValue> SetDefault(TValue value)
        {
            _defaultCase = GetExpressionAssignAndReturn(_outValue, value, false);
            return this;
        }
        private BlockExpression GetExpressionAssignAndReturn(ParameterExpression toAsign, TValue assignValue, bool returnValue)
        {
            ConstantExpression valueExpression = Expression.Constant(assignValue, typeof(TValue));
            BinaryExpression asignOutValue = Expression.Assign(toAsign, valueExpression);
            ConstantExpression returnValueExpression = Expression.Constant(returnValue, typeof(bool));
            BlockExpression blockExpression = Expression.Block(asignOutValue, returnValueExpression);
            return blockExpression;
        }
    }
}
