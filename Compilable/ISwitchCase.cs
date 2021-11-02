using System.Linq.Expressions;

namespace Compilable
{
    public interface ISwitchCase<TKey, TValue>
    {
        Expression GetExpression();
        bool TryGetCase(TKey key, out TValue value);
    }
}