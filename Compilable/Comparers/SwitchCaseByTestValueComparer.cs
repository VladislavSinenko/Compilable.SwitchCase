using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Compilable.Comparers
{
    internal class SwitchCaseByTestValueComparer : IEqualityComparer<SwitchCase>
    {
        public bool Equals(SwitchCase x, SwitchCase y)
        {
            return GetHashCode(x) == GetHashCode(y);
        }

        public int GetHashCode(SwitchCase switchCase)
        {
            return ((ConstantExpression)switchCase.TestValues[0]).Value.GetHashCode();
        }
    }
}
