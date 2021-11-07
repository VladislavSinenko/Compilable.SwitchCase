using Compilable.Comparers;
using Compilable.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Builders
{
    public class SwitchCaseBuilder<TCase, TValue> : ISwitchCaseBuilder<TCase, TValue> // todo: use dependencies for internal logic
    {
        private ParameterExpression _caseValue = Expression.Parameter(typeof(TCase), nameof(_caseValue));
        private ParameterExpression _outValue = Expression.Parameter(typeof(TValue).MakeByRefType(), nameof(_outValue));
        private IEqualityComparer<SwitchCase> switchCaseComparer;
        private ISet<SwitchCase> _cases;
        private BlockExpression _defaultCase = null;
        public SwitchCaseBuilder()
        {
            switchCaseComparer = new SwitchCaseByTestValueComparer();
            _cases = new HashSet<SwitchCase>(switchCaseComparer);
        }
        internal SwitchCaseBuilder(IEqualityComparer<SwitchCase> comparer = null, ISet<SwitchCase> cases = null)
        {
            switchCaseComparer = comparer ?? new SwitchCaseByTestValueComparer();
            _cases = cases ?? new HashSet<SwitchCase>(switchCaseComparer);
        }
        public bool AddCase(TCase _case, TValue value)
        {
            BlockExpression caseBlock = GetExpressionAssignAndReturn(_outValue, value, true);
            ConstantExpression testValue = Expression.Constant(_case, typeof(TCase));
            SwitchCase switchCase = Expression.SwitchCase(caseBlock, testValue);
            return _cases.Add(switchCase);
        }
        public ISwitchCaseProvider<TCase, TValue> GetSwitchCase()
        {
            _defaultCase = _defaultCase ?? GetExpressionAssignAndReturn(_outValue, default(TValue), false);
            SwitchExpression switchExpression = Expression.Switch(_caseValue, _defaultCase, _cases.ToArray());
            BlockExpression blockExpression = Expression.Block(switchExpression);
            Expression<TryGetDelegate<TCase, TValue>> lambdaExpression = Expression.Lambda<TryGetDelegate<TCase, TValue>>(blockExpression, _caseValue, _outValue);
            return new SwitchCaseProvider<TCase, TValue>(lambdaExpression);
        }

        public bool RemoveCase(TCase _case)
        {
            return _cases.Remove(Expression.SwitchCase(null, new[] { Expression.Constant(_case) }));
        }

        public bool SetDefault(TValue value)
        {
            if(_defaultCase == null)
            {
                _defaultCase = GetExpressionAssignAndReturn(_outValue, value, false);
                return true;
            }
            else
            {
                var previousDefault = _defaultCase;
                ConstantExpression valueExpression = Expression.Constant(value, typeof(TValue));
                Expression asignOutValue = Expression.Assign(_outValue, valueExpression);
                Expression returnValueExpression = Expression.Constant(false, typeof(bool));
                _defaultCase = _defaultCase.Update(_defaultCase.Variables, new[] { asignOutValue, returnValueExpression });
                return previousDefault == _defaultCase;
            }
        }

        public bool UpdateCase(TCase _case, TValue value)
        {
            ConstantExpression[] testValue = new[] { Expression.Constant(_case, typeof(TCase)) };
            SwitchCase switchCase = Expression.SwitchCase(null, testValue);
            bool contains = _cases.Contains(switchCase);
            if (contains)
            {
                BlockExpression body = GetExpressionAssignAndReturn(_outValue, value, true);
                switchCase.Update(testValue, body);
                _cases.Remove(switchCase);
                _cases.Add(switchCase);
            }
            return contains;
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
