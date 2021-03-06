using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Strategies
{
    internal class GetConstantValueStrategy : IGetExpressionValue
    {
        public TValue GetValue<TValue>(Expression expression)
        {
            return (TValue)((ConstantExpression)expression).Value;
        }
    }
}
