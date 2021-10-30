namespace Compilable.Builders
{
    public interface IExpressionSwitchCaseBuilder<TKey, TValue> : ISwitchCaseBuilder<TKey, TValue>
    {
        IExpressionSwitchCase<TKey, TValue> GetExpressionSwitchCase();
    }
}