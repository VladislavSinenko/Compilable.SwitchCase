using Compilable.Builders;

namespace Compilable.Strategies
{
    public interface ISwitchCaseBuildingStrategyBase<TCase, TValue> : ISwitchCaseBuildingStrategy<TCase, TValue>
    {
        ISwitchCaseBuilder<TCase, TValue> GetBuilder();
        ISwitchCaseProvider<TCase, TValue> GetSwitchCase();
    }
}