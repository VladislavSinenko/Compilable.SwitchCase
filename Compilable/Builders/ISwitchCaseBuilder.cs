using System;

namespace Compilable.Builders
{
    /// <summary>
    /// Interface represents ISwitchCaseBuilder
    /// </summary>
    /// <typeparam name="TCase">Type of case that used as key to get value</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public interface ISwitchCaseBuilder<TCase, TValue>
    {
        /// <summary>
        /// Adds new item to SwitchCase expression
        /// </summary>
        /// <param name="_case"></param>
        /// <param name="value"></param>
        /// <returns>Returns true if added. Returns false if given case already presents in current SwitchCase expression</returns>
        bool AddSingletonCase(TCase _case, TValue value);
        /// <summary>
        /// Adds new item to SwitchCase expression
        /// </summary>
        /// <param name="_case"></param>
        /// <param name="getValueFunc"></param>
        /// <returns>Returns true if added. Returns false if given case already presents in current SwitchCase expression</returns>
        bool AddCase(TCase _case, Func<TValue> getValueFunc);
        /// <summary>
        /// Update existing item in current SwitchCase expression
        /// </summary>
        /// <param name="_case"></param>
        /// <param name="value"></param>
        /// <returns>Returns true if updated. Returns false if given case not presents in current SwitchCase expression</returns>
        bool UpdateCaseAsSingleton(TCase _case, TValue value);
        /// <summary>
        /// When implemented update existing item in current SwitchCase expression
        /// </summary>
        /// <param name="_case"></param>
        /// <param name="getValueFunc"></param>
        /// <returns>Returns true if updated. Returns false if given case not presents in current SwitchCase expression</returns>
        bool UpdateCase(TCase _case, Func<TValue> getValueFunc);
        /// <summary>
        /// When implemented remove existing item from current SwitchCase expression
        /// </summary>
        /// <param name="_case"></param>
        /// <returns>Returns true if removed. Returns false if given case not presents in current SwitchCase expression</returns>
        bool RemoveCase(TCase _case);
        /// <summary>
        /// Makes current SwitchCase expression return specific default value
        /// </summary>
        /// <param name="value"></param>
        void SetDefaultAsSingleton(TValue value);
        /// <summary>
        /// Makes current SwitchCase expression return specific default value
        /// </summary>
        /// <param name="getValueFunc"></param>
        void SetDefault(Func<TValue> getValueFunc);
        /// <summary>
        /// Create ISwitchCaseProvider which contains configured SwitchCase expression
        /// </summary>
        /// <returns>Returns ISwitchCaseProvider which contains configured SwitchCase expression</returns>
        ISwitchCaseProvider<TCase, TValue> GetSwitchCase();
    }
}