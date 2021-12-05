using Compilable.Builders;
using Compilable.Delegates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Proxies
{
    public class ProxySwitchCaseProvider<TCase, TValue> : ISwitchCaseProvider<TCase, TValue>, IProxySwitchCaseProvider<TCase, TValue>
    {
        private readonly ISwitchCaseBuilder<TCase, TValue> builder;
        private ISwitchCaseProvider<TCase, TValue> switchCase;
        public ISwitchCaseProvider<TCase, TValue> SwitchCase => switchCase ?? (switchCase = builder.GetSwitchCase());
        public ProxySwitchCaseProvider(ISwitchCaseBuilder<TCase, TValue> builder)
        {
            this.builder = builder;
        }
        public TryGetDelegate<TCase, TValue> GetDelegate()
        {
            return SwitchCase.GetDelegate();
        }
        public Expression<TryGetDelegate<TCase, TValue>> GetExpression()
        {
            return SwitchCase.GetExpression();
        }
    }
}
