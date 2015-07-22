namespace Supermarket.ConsoleApp.Interfaces
{
    internal interface ICommand : IExecutable
    {
        IEngine Engine { get; }
    }
}