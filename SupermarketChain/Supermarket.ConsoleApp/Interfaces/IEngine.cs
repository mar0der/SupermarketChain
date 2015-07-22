namespace Supermarket.ConsoleApp.Interfaces
{
    #region

    using System.Text;

    #endregion

    public interface IEngine
    {
        bool HasStarted { get; set; }

        StringBuilder Output { get; }

        void Run();
    }
}