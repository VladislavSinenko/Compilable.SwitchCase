using System.Linq.Expressions;

namespace Compilable
{
    internal interface IExpressionable
    {
        Expression GetExpression();
    }
}