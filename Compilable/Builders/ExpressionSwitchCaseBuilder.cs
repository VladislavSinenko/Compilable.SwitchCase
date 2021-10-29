using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Builders
{
    public class ExpressionSwitchCaseBuilder<TKey, TValue> : ISwitchCaseBuilder<TKey, TValue>, IExpressionSwitchCaseBuilder<TKey, TValue>
    {
        public ISwitchCaseBuilder<TKey, TValue> AddCase(TKey @case, TKey value)
        {
            throw new NotImplementedException();
        }

        public ISwitchCase<TKey, TValue> GetSwitchCase()
        {
            throw new NotImplementedException();
        }

        public ExpressionSwitchCase<TKey, TValue> GetExpressionSwitchCase()
        {
            return (ExpressionSwitchCase<TKey, TValue>)GetSwitchCase();
        }

        public ISwitchCaseBuilder<TKey, TValue> SetDefault(TKey value)
        {
            throw new NotImplementedException();
        }
    }
}
