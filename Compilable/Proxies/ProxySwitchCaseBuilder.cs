using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Proxies
{
    public class ProxySwitchCaseBuilder<TCase, TValue> : ISwitchCaseBuilder<TCase, TValue>
    {
        private ISwitchCaseBuilder<TCase, TValue> builder;
        private ISwitchCaseProvider<TCase, TValue> switchCase;
        public ProxySwitchCaseBuilder(ISwitchCaseBuilder<TCase, TValue> builder)
        {
            this.builder = builder;
        }
        public bool AddCase(TCase _case, TValue value)
        {
            bool result = builder.AddCase(_case, value);

            if(result)
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

        public bool SetDefault(TValue value)
        {
            var result = builder.SetDefault(value);

            if (result)
                switchCase = null;

            return result;
        }

        public bool UpdateCase(TCase _case, TValue value)
        {
            var result = builder.UpdateCase(_case, value);

            if (result)
                switchCase = null;

            return result;
        }
    }
}
