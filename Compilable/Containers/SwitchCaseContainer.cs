using Compilable.Builders;
using Compilable.Strategies;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Containers
{
    internal static class SwitchCaseContainer
    {
        public static readonly ISwitchCaseProvider<ExpressionType, IGetExpressionValue> GetExpressionProvider;
        static SwitchCaseContainer()
        {
            var strategy = new GetExpressionValueBuildingStrategy();
            GetExpressionProvider = strategy.GetSwitchCase();
        }
    }
}
