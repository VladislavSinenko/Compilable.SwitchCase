using Compilable.Builders;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Strategies
{
    internal class GetExpressionValueBuildingStrategy : SwitchCaseBuildingStrategyBase<ExpressionType, IGetExpressionValue>
    {
        public override ISwitchCaseBuilder<ExpressionType, IGetExpressionValue> ConfigureBuilder(ISwitchCaseBuilder<ExpressionType, IGetExpressionValue> builder)
        {
            builder.AddCase(ExpressionType.Constant, () => new GetConstantValueStrategy());
            builder.AddCase(ExpressionType.Call, () => new GetExpressionCallStrategy());
            return builder;
        }
    }
}
