namespace Compilable
{
    public interface ISwitchCaseBuilder<TKey, TValue>
    {
        ISwitchCaseBuilder<TKey, TValue> AddCase(TKey @case, TKey value);
        ISwitchCaseBuilder<TKey, TValue> SetDefault(TKey value);
        ISwitchCase<TKey, TValue> GetSwitchCase();
    }
}