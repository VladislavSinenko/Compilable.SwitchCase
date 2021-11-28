using System;
using System.Collections.Generic;
using System.Text;

namespace Compilable.Delegates
{
    /// <summary>
    /// Delegate that represents TryGet function
    /// </summary>
    /// <typeparam name="TCase">Type of key</typeparam>
    /// <typeparam name="TValue">Type of out value</typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns>Returns true if success. Othervice false.</returns>
    public delegate bool TryGetDelegate<TCase, TValue>(TCase key, out TValue value);
}
