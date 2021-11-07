using Compilable.Delegates;
using System;
using System.Linq.Expressions;

namespace Compilable
{
    public class SwitchCaseProvider<TCase, TValue> : ISwitchCaseProvider<TCase, TValue>
    {
        private TryGetDelegate<TCase, TValue> _delegate;
        private Expression<TryGetDelegate<TCase, TValue>> expression;
        public SwitchCaseProvider(Expression<TryGetDelegate<TCase, TValue>> expression)
        {
            this.expression = expression;
            _delegate = expression.Compile();
        }
        public Expression<TryGetDelegate<TCase, TValue>> GetExpression()
        {
            return expression;
        }
        public TryGetDelegate<TCase, TValue> GetDelegate()
        {
            return _delegate;
        }
    }
}
