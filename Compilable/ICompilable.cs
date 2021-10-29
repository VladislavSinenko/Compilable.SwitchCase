namespace Compilable
{
    internal interface ICompilable
    {
        bool IsCompiled { get; }
        void Compile();
    }
}