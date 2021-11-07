using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Factories
{
    public interface ISwitchCaseFactory
    {
        bool AddSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key);
        bool AddSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key);
        bool UpdateSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key);
        bool UpdateSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key);
        bool RemoveSwitchCase<TCase, TValue>(string key);
        bool TryGetBuilder<TCase, TValue>(string key, out ISwitchCaseBuilder<TCase, TValue> builder);
        bool TryGetDelegate<TCase, TValue>(string key, out TryGetDelegate<TCase, TValue> _delegate);
        bool TryGetProvider<TCase, TValue>(string key, out ISwitchCaseProvider<TCase, TValue> provider);
    }
}
