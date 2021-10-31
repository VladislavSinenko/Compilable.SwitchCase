using Compilable.Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable
{
    public sealed class ExpressionSwitchCase<TKey, TValue> : IExpressionSwitchCase<TKey, TValue>, ISwitchCase<TKey, TValue>
    {
        private TryGetDelegate<TKey, TValue> _tryGet;
        private Expression<TryGetDelegate<TKey, TValue>> _metadata;
        public ExpressionSwitchCase(Expression<TryGetDelegate<TKey, TValue>> expression)
        {
            _metadata = expression;
            _tryGet = expression.Compile();
        }
        public Expression GetExpression()
        {
            return _metadata;
        }
        public bool TryGetCase(TKey key, out TValue value)
        {
            return _tryGet.Invoke(key, out value);
        }
    }
}
