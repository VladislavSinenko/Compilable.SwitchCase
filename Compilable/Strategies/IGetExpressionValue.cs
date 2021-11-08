using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Strategies
{
    public interface IGetExpressionValue
    {
        TValue GetValue<TValue>(Expression expression);
    }
}
