namespace Compilable.Builders
{
    public interface ISwitchCaseBuilder<TCase, TValue>
    {
        bool AddCase(TCase _case, TValue value);
        bool UpdateCase(TCase _case, TValue value);
        bool RemoveCase(TCase _case);
        bool SetDefault(TValue value);
        ISwitchCaseProvider<TCase, TValue> GetSwitchCase();
    }
}