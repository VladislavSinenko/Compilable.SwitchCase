namespace Compilable.Proxies
{
    public interface IProxySwitchCaseProvider<TCase, TValue>
    {
        ISwitchCaseProvider<TCase, TValue> SwitchCase { get; }
    }
}