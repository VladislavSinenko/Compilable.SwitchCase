using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable
{
    public delegate bool TryGetDelegate<TKey, TValue>(TKey key, TValue value);
    public class ExpressionSwitchCase<TKey, TValue> : ISwitchCase<TKey, TValue>, ICompilable, IExpressionable
    {
        private TryGetDelegate<TKey, TValue> _tryGet;
        public bool IsCompiled => _tryGet != null;
        public void Compile()
        {
            var expression = GetExpression();
        }
        public Expression GetExpression()
        {
            throw new NotImplementedException();
        }
        public bool TryGetCase(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }
    }
}
