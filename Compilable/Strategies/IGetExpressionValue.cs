using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Strategies
{
    /// <summary>
    /// Represents interface for get value from Expression strategy
    /// </summary>
    public interface IGetExpressionValue
    {
        /// <summary>
        /// Method get value from given expression
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TValue GetValue<TValue>(Expression expression);
    }
}
