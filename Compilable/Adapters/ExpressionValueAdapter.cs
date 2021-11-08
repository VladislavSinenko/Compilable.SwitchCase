using Compilable.Delegates;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Adapters
{
    public class ExpressionValueAdapter : IExpressionValueAdapter
    {
        private TryGetDelegate<ExpressionType, IGetExpressionValue> tryGetStrategy;
        public ExpressionValueAdapter(ISwitchCaseProvider<ExpressionType, IGetExpressionValue> strategyProvider)
        {
            this.tryGetStrategy = strategyProvider.GetDelegate();
        }
        public TValue GetValue<TValue>(Expression expression)
        {
            if(tryGetStrategy(expression.NodeType, out IGetExpressionValue strategy))
                return strategy.GetValue<TValue>(expression);

            throw new ArgumentException($"NodeType: {expression.NodeType} is not defined in SwitchCaseProvider");
        }
    }
}
