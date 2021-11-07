using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Strategies
{
    public class DelegateSwitchCaseBuildingStrategy<TCase, TValue> : ISwitchCaseBuildingStrategy<TCase, TValue>
    {
        private Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> implementationFunc;
        public DelegateSwitchCaseBuildingStrategy(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> implementationFunc)
        {
            this.implementationFunc = implementationFunc;
        }
        public ISwitchCaseBuilder<TCase, TValue> ConfigureBuilder(ISwitchCaseBuilder<TCase, TValue> builder)
        {
            return implementationFunc(builder);
        }
    }
}
