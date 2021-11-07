using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Delegates
{
    public delegate bool TryGetDelegate<TCase, TValue>(TCase key, out TValue value);
}
