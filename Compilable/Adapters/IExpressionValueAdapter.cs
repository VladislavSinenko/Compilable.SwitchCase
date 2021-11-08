using System.Linq.Expressions;

namespace Compilable.Adapters
{
    public interface IExpressionValueAdapter
    {
        TValue GetValue<TValue>(Expression expression);
    }
}