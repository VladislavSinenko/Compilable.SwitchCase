namespace Compilable.Builders
{
    public interface ISwitchCaseBuilder<TKey, TValue>
    {
        ISwitchCaseBuilder<TKey, TValue> AddCase(TKey @case, TKey value);
        ISwitchCaseBuilder<TKey, TValue> SetDefault(TValue value);
        ISwitchCase<TKey, TValue> GetSwitchCase();
    }
}