using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Strategies
{
    public abstract class SwitchCaseBuildingStrategyBase<TCase, TValue> : ISwitchCaseBuildingStrategyBase<TCase, TValue>
    {
        public abstract ISwitchCaseBuilder<TCase, TValue> ConfigureBuilder(ISwitchCaseBuilder<TCase, TValue> builder);
        public virtual ISwitchCaseBuilder<TCase, TValue> GetBuilder()
        {
            return new SwitchCaseBuilder<TCase, TValue>();
        }
        public virtual ISwitchCaseProvider<TCase, TValue> GetSwitchCase()
        {
            var builder = GetBuilder();
            builder = ConfigureBuilder(builder);
            var provider = builder.GetSwitchCase();
            return provider;
        }
    }
}
