using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Proxies;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Factories
{
    /// <summary>
    /// Represents SwitchCaseFactory 
    /// </summary>
    public class SwitchCaseFactory : ISwitchCaseFactory
    {
        private ISwitchCaseProvider<string, object> getBuilderProvider;
        private ISwitchCaseBuilder<string, object> _builder;
        /// <summary>
        /// Create SwitchCaseFactory
        /// </summary>
        public SwitchCaseFactory()
        {
            _builder = new ProxySwitchCaseBuilder<string, object>(new SwitchCaseBuilder<string, object>());
            getBuilderProvider = new ProxySwitchCaseProvider<string, object>(_builder);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="builderConfiguration"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool AddSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key)
        {
            var strategy = new DelegateSwitchCaseBuildingStrategy<TCase, TValue>(builderConfiguration);
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.AddSingletonCase(key, strategy.ConfigureBuilder(builder));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="strategy"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool AddSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key)
        {
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.AddSingletonCase(key, strategy.ConfigureBuilder(builder));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool RemoveSwitchCase<TCase, TValue>(string key)
        {
            return _builder.RemoveCase(key);
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="builder"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool TryGetBuilder<TCase, TValue>(string key, out ISwitchCaseBuilder<TCase, TValue> builder)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object existedBuilder);
            builder = (ISwitchCaseBuilder<TCase, TValue>)existedBuilder;
            return isExist;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="_delegate"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool TryGetDelegate<TCase, TValue>(string key, out TryGetDelegate<TCase, TValue> _delegate)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object builder);

            _delegate = ((ISwitchCaseBuilder<TCase, TValue>)builder)?
                .GetSwitchCase()
                .GetDelegate();

            return isExist;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="key"><inheritdoc/></param>
        /// <param name="provider"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool TryGetProvider<TCase, TValue>(string key, out ISwitchCaseProvider<TCase, TValue> provider)
        {
            var isExist = getBuilderProvider.GetDelegate()(key, out object builder);
            provider = ((ISwitchCaseBuilder<TCase, TValue>)builder)?.GetSwitchCase();
            return isExist;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="builderConfiguration"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool UpdateSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key)
        {
            var strategy = new DelegateSwitchCaseBuildingStrategy<TCase, TValue>(builderConfiguration);
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.UpdateCaseAsSingleton(key, strategy.ConfigureBuilder(builder));
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <typeparam name="TCase"><inheritdoc/></typeparam>
        /// <typeparam name="TValue"><inheritdoc/></typeparam>
        /// <param name="strategy"><inheritdoc/></param>
        /// <param name="key"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public bool UpdateSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key)
        {
            var builder = new SwitchCaseBuilder<TCase, TValue>();
            return _builder.UpdateCaseAsSingleton(key, strategy.ConfigureBuilder(builder));
        }
    }
}
