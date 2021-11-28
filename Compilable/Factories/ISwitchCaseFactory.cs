using Compilable.Builders;
using Compilable.Delegates;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Factories
{
    /// <summary>
    /// Interface represents SwitchCaseFactory
    /// </summary>
    public interface ISwitchCaseFactory
    {
        /// <summary>
        /// Creates and add SwitchCase provider to collection by given builderConfiguration Func
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="builderConfiguration">Func that configre and return ISwitchCaseBuilder</param>
        /// <param name="key">Key for storing the item in the collection</param>
        /// <returns>Returns true if new item added in collection. Othervice false.</returns>
        bool AddSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key);
        /// <summary>
        /// Creates and add SwitchCase provider to collection by given ISwitchCaseBuildingStrategy
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="strategy">Strategy to build ISwitchCaseBuilder</param>
        /// <param name="key">Key for storing the item in the collection</param>
        /// <returns>Returns true if new item added in collection. False if element already contains.</returns>
        bool AddSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key);
        /// <summary>
        /// Creates and update SwitchCase provider to collection by given builderConfiguration Func
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="builderConfiguration">Func that configre and return ISwitchCaseBuilder</param>
        /// <param name="key">Key for storing the item in the collection</param>
        /// <returns>Returns true if item was updated in collection. False if element do not contains in collection.</returns>
        bool UpdateSwitchCase<TCase, TValue>(Func<ISwitchCaseBuilder<TCase, TValue>, ISwitchCaseBuilder<TCase, TValue>> builderConfiguration, string key);
        /// <summary>
        /// Creates and update SwitchCase provider to collection by given ISwitchCaseBuildingStrategy
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="strategy">Strategy to build ISwitchCaseBuilder</param>
        /// <param name="key">Key for storing the item in the collection</param>
        /// <returns>Returns true if item was updated in collection. False if element do not contains in collection.</returns>
        bool UpdateSwitchCase<TCase, TValue>(ISwitchCaseBuildingStrategy<TCase, TValue> strategy, string key);
        /// <summary>
        /// Removes SwitchCase from collection
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <returns>True if item was deleted. Othervice false.</returns>
        bool RemoveSwitchCase<TCase, TValue>(string key);
        /// <summary>
        /// Method get builder by given key
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="builder"></param>
        /// <returns>Returns true if builder have been found. False if default value returned</returns>
        bool TryGetBuilder<TCase, TValue>(string key, out ISwitchCaseBuilder<TCase, TValue> builder);
        /// <summary>
        /// Method get delegate by given key
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="_delegate"></param>
        /// <returns>Returns true if delegate have been found. False if default value returned</returns>
        bool TryGetDelegate<TCase, TValue>(string key, out TryGetDelegate<TCase, TValue> _delegate);
        /// <summary>
        /// Method get provider by given key
        /// </summary>
        /// <typeparam name="TCase"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="provider"></param>
        /// <returns>Returns true if provider have been found. False if default value returned</returns>
        bool TryGetProvider<TCase, TValue>(string key, out ISwitchCaseProvider<TCase, TValue> provider);
    }
}
