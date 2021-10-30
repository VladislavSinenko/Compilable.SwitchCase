namespace Compilable
{
    IExpressionSwitchCase interface ICompilable
    {
        bool IsCompiled { get; }
        void Compile();
    }
}