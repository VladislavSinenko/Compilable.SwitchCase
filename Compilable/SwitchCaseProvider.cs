using Compilable.Delegates;
using System;
using System.Linq.Expressions;

namespace Compilable
{
    /// <summary>
    /// Represents SwitchCaseProvider
    /// </summary>
    /// <typeparam name="TCase"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SwitchCaseProvider<TCase, TValue> : ISwitchCaseProvider<TCase, TValue>
    {
        private TryGetDelegate<TCase, TValue> _delegate;
        private Expression<TryGetDelegate<TCase, TValue>> expression;
        internal SwitchCaseProvider(Expression<TryGetDelegate<TCase, TValue>> expression)
        {
            this.expression = expression;
            _delegate = expression.Compile();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public Expression<TryGetDelegate<TCase, TValue>> GetExpression()
        {
            return expression;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public TryGetDelegate<TCase, TValue> GetDelegate()
        {
            return _delegate;
        }
    }
}
