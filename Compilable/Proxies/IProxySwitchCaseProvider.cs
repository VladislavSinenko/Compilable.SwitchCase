namespace Compilable.Proxies
{
    internal interface IProxySwitchCaseProvider<TCase, TValue>
    {
        ISwitchCaseProvider<TCase, TValue> SwitchCase { get; }
    }
}