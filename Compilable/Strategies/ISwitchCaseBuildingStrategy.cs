using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Strategies
{
    public interface ISwitchCaseBuildingStrategy<TCase, TValue>
    {
        ISwitchCaseBuilder<TCase, TValue> ConfigureBuilder(ISwitchCaseBuilder<TCase, TValue> builder);
    }
}
