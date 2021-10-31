using System.Linq.Expressions;

namespace Compilable
{
    public interface IExpressionSwitchCase<TKey, TValue> : ISwitchCase<TKey, TValue>
    {
        Expression GetExpression();
    }
}