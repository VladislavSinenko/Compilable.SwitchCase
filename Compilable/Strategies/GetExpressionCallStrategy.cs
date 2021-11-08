using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Strategies
{
    class GetExpressionCallStrategy : IGetExpressionValue
    {
        public TValue GetValue<TValue>(Expression expression)
        {
            return Expression.Lambda<Func<TValue>>(expression).Compile().Invoke();
        }
    }
}
