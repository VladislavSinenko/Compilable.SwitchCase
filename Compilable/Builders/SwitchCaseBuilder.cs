using Compilable.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Builders
{
    /// <summary>
    /// Represents class that build ISwitchCaseProvider
    /// </summary>
    /// <typeparam name="TCase"><inheritdoc/></typeparam>
    /// <typeparam name="TValue"><inheritdoc/></typeparam>
    public class SwitchCaseBuilder<TCase, TValue> : ISwitchCaseBuilder<TCase, TValue> // todo: use dependencies for internal logic
    {
        private ParameterExpression _caseValue = Expression.Parameter(typeof(TCase), nameof(_caseValue));
        private ParameterExpression _outValue = Expression.Parameter(typeof(TValue).MakeByRefType(), nameof(_outValue));
        private IDictionary<TCase, SwitchCase> _cases;
        private BlockExpression _defaultCase = null;
        /// <summary>
        /// Creates SwitchCaseBuilder
        /// </summary>
        public SwitchCaseBuilder()
        {
            _cases = new Dictionary<TCase, SwitchCase>();
        }
        /// <summary>
        /// Creates SwitchCaseBuilder with given IEqualityComparer
        /// </summary>
        /// <param name="caseComparer">IEqualityComparer to compare cases when add update or delete</param>
        public SwitchCaseBuilder(IEqualityComparer<TCase> caseComparer)
        {
            _cases = new Dictionary<TCase, SwitchCase>(caseComparer);
        }
        internal SwitchCaseBuilder(IDictionary<TCase, SwitchCase> cases)
        {
            _cases = cases;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="_case"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool AddSingletonCase(TCase _case, TValue value)
        {
            bool contains = _cases.ContainsKey(_case);

            if (!contains)
            {
                BlockExpression caseBlock = GetExpressionAssignAndReturn(_outValue, value, true);
                ConstantExpression testValue = Expression.Constant(_case, typeof(TCase));
                SwitchCase switchCase = Expression.SwitchCase(caseBlock, testValue);
                _cases.Add(_case, switchCase);
            }

            return contains;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="_case"><inheritdoc/></param>
        /// <param name="getValueFunc"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool AddCase(TCase _case, Func<TValue> getValueFunc)
        {
            bool contains = _cases.ContainsKey(_case);

            if (!contains)
            {
                ConstantExpression testValue = Expression.Constant(_case, typeof(TCase));
                BlockExpression body = GetExpressionAssignAndReturn(_outValue, getValueFunc, true);
                SwitchCase switchCase = Expression.SwitchCase(body, testValue);
                _cases.Add(_case, switchCase);
            }

            return contains;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public ISwitchCaseProvider<TCase, TValue> GetSwitchCase()
        {
            if (_defaultCase == null)
                _defaultCase = GetExpressionAssignAndReturn(_outValue, default(TValue), false);

            SwitchExpression switchExpression = Expression.Switch(_caseValue, _defaultCase, _cases.Select(kv => kv.Value).ToArray());
            BlockExpression blockExpression = Expression.Block(switchExpression);
            Expression<TryGetDelegate<TCase, TValue>> lambdaExpression = Expression.Lambda<TryGetDelegate<TCase, TValue>>(blockExpression, _caseValue, _outValue);
            return new SwitchCaseProvider<TCase, TValue>(lambdaExpression);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="_case"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool RemoveCase(TCase _case)
        {
            return _cases.Remove(_case);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="value"><inheritdoc/></param>
        public void SetDefaultAsSingleton(TValue value)
        {
            _defaultCase = GetExpressionAssignAndReturn(_outValue, value, false);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="getValueFunc"><inheritdoc/></param>
        public void SetDefault(Func<TValue> getValueFunc)
        {
            _defaultCase = GetExpressionAssignAndReturn(_outValue, getValueFunc, false);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="_case"><inheritdoc/></param>
        /// <param name="value"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool UpdateCaseAsSingleton(TCase _case, TValue value)
        {
            bool contains = _cases.ContainsKey(_case);

            if (contains)
            {
                ConstantExpression testValue = Expression.Constant(_case, typeof(TCase));
                BlockExpression body = GetExpressionAssignAndReturn(_outValue, value, true);
                SwitchCase switchCase = Expression.SwitchCase(body, testValue);
                _cases.Remove(_case);
                _cases.Add(_case, switchCase);
            }

            return contains;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="_case"><inheritdoc/></param>
        /// <param name="getValueFunc"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool UpdateCase(TCase _case, Func<TValue> getValueFunc)
        {
            bool contains = _cases.ContainsKey(_case);

            if (contains)
            {
                ConstantExpression testValue = Expression.Constant(_case, typeof(TCase));
                BlockExpression body = GetExpressionAssignAndReturn(_outValue, getValueFunc, true);
                SwitchCase switchCase = Expression.SwitchCase(body, testValue);
                _cases.Remove(_case);
                _cases.Add(_case, switchCase);
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

        private BlockExpression GetExpressionAssignAndReturn(ParameterExpression toAsign, Func<TValue> getAssignValue, bool returnValue)
        {
            ConstantExpression targetExpression = Expression.Constant(getAssignValue.Target);
            MethodCallExpression getValueCallExpression = Expression.Call(targetExpression, getAssignValue.Method);
            BinaryExpression asignOutValue = Expression.Assign(toAsign, getValueCallExpression);
            ConstantExpression returnValueExpression = Expression.Constant(returnValue, typeof(bool));
            BlockExpression body = Expression.Block(asignOutValue, returnValueExpression);
            return body;
        }
    }
}
