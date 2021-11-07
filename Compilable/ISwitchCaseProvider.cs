using Compilable.Delegates;
using System.Linq.Expressions;

namespace Compilable
{
    public interface ISwitchCaseProvider<TCase, TValue>
    {
        Expression<TryGetDelegate<TCase, TValue>> GetExpression();
        TryGetDelegate<TCase, TValue> GetDelegate();
    }
}