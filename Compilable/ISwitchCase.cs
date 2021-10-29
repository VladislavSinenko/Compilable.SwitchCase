namespace Compilable
{
    public interface ISwitchCase<TKey, TValue>
    {
        bool TryGetCase(TKey key, out TValue value);
    }
}