using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Strategies
{
    /// <summary>
    /// Represents interface for SwitchCaseBuildingStrategy
    /// </summary>
    /// <typeparam name="TCase"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ISwitchCaseBuildingStrategy<TCase, TValue>
    {
        /// <summary>
        /// Method configure ISwitchCaseBuilder
        /// </summary>
        /// <param name="builder">Builder to configure</param>
        /// <returns>Return configured builder</returns>
        ISwitchCaseBuilder<TCase, TValue> ConfigureBuilder(ISwitchCaseBuilder<TCase, TValue> builder);
    }
}
