using Compilable.Builders;

namespace Compilable.Strategies
{
    /// <summary>
    /// Represents interface for SwitchCaseBuildingStrategyBase
    /// </summary>
    /// <typeparam name="TCase"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ISwitchCaseBuildingStrategyBase<TCase, TValue> : ISwitchCaseBuildingStrategy<TCase, TValue>
    {
        /// <summary>
        /// Method create default builder
        /// </summary>
        /// <returns>Return default builder</returns>
        ISwitchCaseBuilder<TCase, TValue> GetBuilder();
        /// <summary>
        /// Method create SwitchCase from configured builder
        /// </summary>
        /// <returns>Returns SwitchCase from configured builder</returns>
        ISwitchCaseProvider<TCase, TValue> GetSwitchCase();
    }
}