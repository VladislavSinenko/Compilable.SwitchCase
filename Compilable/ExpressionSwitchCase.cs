using Compilable.Delegates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable
{
    public class ExpressionSwitchCase<TKey, TValue> : IExpressionSwitchCase<TKey, TValue>, ISwitchCase<TKey, TValue>, ICompilable
    {
        private TryGetDelegate<TKey, TValue> _tryGet;
        private Expression switchCaseExpression;
        public bool IsCompiled => _tryGet != null;
        public void Compile()
        {
            throw new NotImplementedException();
        }
        public Expression GetExpression()
        {
            return switchCaseExpression;
        }
        public bool TryGetCase(TKey key, out TValue value)
        {
            return _tryGet.Invoke(key, out value);
        }
    }
}
