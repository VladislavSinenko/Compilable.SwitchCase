using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Proxies
{
    internal class ProxySwitchCaseBuilder<TCase, TValue> : ISwitchCaseBuilder<TCase, TValue>
    {
        private ISwitchCaseBuilder<TCase, TValue> builder;
        private ISwitchCaseProvider<TCase, TValue> switchCase;
        public ProxySwitchCaseBuilder(ISwitchCaseBuilder<TCase, TValue> builder)
        {
            this.builder = builder;
        }
        public bool AddSingletonCase(TCase _case, TValue value)
        {
            bool result = builder.AddSingletonCase(_case, value);

            if(result)
                switchCase = null;

            return result;
        }

        public bool AddCase(TCase _case, Func<TValue> getValueFunc)
        {
            bool result = builder.AddCase(_case, getValueFunc);

            if (result)
                switchCase = null;

            return result;
        }

        public ISwitchCaseProvider<TCase, TValue> GetSwitchCase()
        {
            return switchCase ?? (switchCase = builder.GetSwitchCase());
        }

        public bool RemoveCase(TCase _case)
        {
            bool result = builder.RemoveCase(_case);

            if (result)
                switchCase = null;

            return result;
        }

        public void SetDefaultAsSingleton(TValue value)
        {
            builder.SetDefaultAsSingleton(value);
            switchCase = null;
        }

        public void SetDefault(Func<TValue> getValueFunc)
        {
            builder.SetDefault(getValueFunc);
            switchCase = null;
        }

        public bool UpdateCaseAsSingleton(TCase _case, TValue value)
        {
            var result = builder.UpdateCaseAsSingleton(_case, value);

            if (result)
                switchCase = null;

            return result;
        }

        public bool UpdateCase(TCase _case, Func<TValue> getValueFunc)
        {
            var result = builder.UpdateCase(_case, getValueFunc);

            if (result)
                switchCase = null;

            return result;
        }
    }
}
