using Compilable.Delegates;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Adapters
{
    /// <summary>
    /// Provides class to get value from Expression by strategy that contains in given strategyProvider
    /// </summary>
    public class ExpressionValueAdapter : IExpressionValueAdapter
    {
        private TryGetDelegate<ExpressionType, IGetExpressionValue> tryGetStrategy;
        /// <summary>
        /// Creates a new instance of ExpressionValueAdapter with given ISwitchCaseProvider
        /// </summary>
        /// <param name="strategyProvider">ISwitchCaseProvider to get strategies from</param>
        public ExpressionValueAdapter(ISwitchCaseProvider<ExpressionType, IGetExpressionValue> strategyProvider)
        {
            this.tryGetStrategy = strategyProvider.GetDelegate();
        }
        /// <summary>
        /// This method gets IGetExpressionValue strategy from strategyProvider and returns value from given expression
        /// </summary>
        /// <typeparam name="TValue">Type of value to return</typeparam>
        /// <param name="expression">Expression to get value from</param>
        /// <returns>TValue from given Expression</returns>
        /// <exception cref="ArgumentException">If expression.NodeType is not contains in ISwitchCaseProvider</exception>
        public TValue GetValue<TValue>(Expression expression)
        {
            if(tryGetStrategy(expression.NodeType, out IGetExpressionValue strategy))
                return strategy.GetValue<TValue>(expression);

            throw new ArgumentException($"NodeType: {expression.NodeType} is not defined in SwitchCaseProvider");
        }
    }
}
