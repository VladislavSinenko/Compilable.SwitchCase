using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Proxies;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Factories
{
    public class SwitchCaseFactory : ISwitchCaseFactory
    {
        private ISwitchCaseProvider<string, object> getBuilderProvider;
        private ISwitchCaseBuilder<string, object> _builder;
        public SwitchCaseFactory()
        {
            _builder = new ProxySwitchCaseBuilder<string, object>(new SwitchCaseBuilder<string, object>());
            getBuilderProvider = new ProxySwitchCaseProvider<string, object>(_builder);
        }
        public bool AddSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key)
        {
            var strategy = new DelegateSwitchCaseBuildingStrategy<TCase, TValue>(builderConfiguration);
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.AddCase(key, strategy.ConfigureBuilder(builder));
        }
        public bool AddSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key)
        {
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.AddCase(key, strategy.ConfigureBuilder(builder));
        }

        public bool RemoveSwitchCase<TCase, TValue>(string key)
        {
            return _builder.RemoveCase(key);
        }

        public bool TryGetBuilder<TCase, TValue>(string key, out ISwitchCaseBuilder<TCase, TValue> builder)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object existedBuilder);
            builder = (ISwitchCaseBuilder<TCase, TValue>)existedBuilder;
            return isExist;
        }

        public bool TryGetDelegate<TCase, TValue>(string key, out TryGetDelegate<TCase, TValue> _delegate)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object builder);

            _delegate = ((ISwitchCaseBuilder<TCase, TValue>)builder)?
                .GetSwitchCase()
                .GetDelegate();

            return isExist;
        }

        public bool TryGetProvider<TCase, TValue>(string key, out ISwitchCaseProvider<TCase, TValue> provider)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object builder);
            provider = ((ISwitchCaseBuilder<TCase, TValue>)builder)?.GetSwitchCase();
            return isExist;
        }

        public bool UpdateSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key)
        {
            var strategy = new DelegateSwitchCaseBuildingStrategy<TCase, TValue>(builderConfiguration);
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.UpdateCase(key, strategy.ConfigureBuilder(builder));
        }

        public bool UpdateSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key)
        {
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.UpdateCase(key, strategy.ConfigureBuilder(builder));
        }
    }
}
