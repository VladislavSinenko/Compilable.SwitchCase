namespace Compilable.Builders
{
    public interface IExpressionSwitchCaseBuilder<TKey, TValue> : ISwitchCaseBuilder<TKey, TValue>
    {
        ExpressionSwitchCase<TKey, TValue> GetExpressionSwitchCase();
    }
}