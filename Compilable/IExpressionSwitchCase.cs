using System.Linq.Expressions;

namespace Compilable
{
    public interface IExpressionSwitchCase<TKey, TValue> : ISwitchCase<TKey, TValue>, ICompilable
    {
        Expression GetExpression();
    }
}