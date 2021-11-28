using Compilable.Delegates;
using System.Linq.Expressions;

namespace Compilable
{
    /// <summary>
    /// Represents interface to provide SwitchCase
    /// </summary>
    /// <typeparam name="TCase"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ISwitchCaseProvider<TCase, TValue>
    {
        /// <summary>
        /// Method get expression that represents SwitchCaseExpression
        /// </summary>
        /// <returns>Returns expression that represents SwitchCaseExpression</returns>
        Expression<TryGetDelegate<TCase, TValue>> GetExpression();
        /// <summary>
        /// Method get delegate that represents SwitchCaseExpression
        /// </summary>
        /// <returns>Returns delegate that represents SwitchCaseExpression</returns>
        TryGetDelegate<TCase, TValue> GetDelegate();
    }
}