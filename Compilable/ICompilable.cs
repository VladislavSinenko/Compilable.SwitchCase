namespace Compilable
{
    public interface ICompilable
    {
        bool IsCompiled { get; }
        void Compile();
    }
}