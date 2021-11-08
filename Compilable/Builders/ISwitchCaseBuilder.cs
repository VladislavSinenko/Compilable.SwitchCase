using System;

namespace Compilable.Builders
{
    public interface ISwitchCaseBuilder<TCase, TValue>
    {
        bool AddSingletonCase(TCase _case, TValue value);
        bool AddCase(TCase _case, Func<TValue> getValueFunc);
        bool UpdateCaseAsSingleton(TCase _case, TValue value);
        bool UpdateCase(TCase _case, Func<TValue> getValueFunc);
        bool RemoveCase(TCase _case);
        void SetDefaultAsSingleton(TValue value);
        void SetDefault(Func<TValue> getValueFunc);
        ISwitchCaseProvider<TCase, TValue> GetSwitchCase();
    }
}