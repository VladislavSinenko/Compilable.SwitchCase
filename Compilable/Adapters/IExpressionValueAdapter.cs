using System.Linq.Expressions;

namespace Compilable.Adapters
{
    /// <summary>
    /// Represents type to get value from Expression
    /// </summary>
    public interface IExpressionValueAdapter
    {
        /// <summary>
        /// When implemented returns value from given Expression
        /// </summary>
        /// <typeparam name="TValue">Type of value to return</typeparam>
        /// <param name="expression">Expression to get value from</param>
        /// <returns>TValue from given Expression</returns>
        TValue GetValue<TValue>(Expression expression);
    }
}