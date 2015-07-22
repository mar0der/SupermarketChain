namespace Supermarket.ConsoleApp.Commands
{
    #region

    using System.Collections.Generic;

    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public abstract class AbstractCommand : ICommand
    {
        public readonly List<string> Data = new List<string>();

        protected AbstractCommand(IEngine engine)
        {
            this.Engine = engine;
        }

        public IEngine Engine { get; private set; }

        public abstract void Execute();
    }
}