using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Strategies
{
    /// <summary>
    /// Represents base class for SwitchCaseBuildingStrategy
    /// </summary>
    /// <typeparam name="TCase"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class SwitchCaseBuildingStrategyBase<TCase, TValue> : ISwitchCaseBuildingStrategyBase<TCase, TValue>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public abstract ISwitchCaseBuilder<TCase, TValue> ConfigureBuilder(ISwitchCaseBuilder<TCase, TValue> builder);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public virtual ISwitchCaseBuilder<TCase, TValue> GetBuilder()
        {
            return new SwitchCaseBuilder<TCase, TValue>();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public virtual ISwitchCaseProvider<TCase, TValue> GetSwitchCase()
        {
            var builder = GetBuilder();
            builder = ConfigureBuilder(builder);
            var provider = builder.GetSwitchCase();
            return provider;
        }
    }
}
