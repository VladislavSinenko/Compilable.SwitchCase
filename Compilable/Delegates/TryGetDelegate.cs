using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Delegates
{
    public delegate bool TryGetDelegate<TKey, TValue>(TKey key, out TValue value);
}
