namespace Supermarket.ConsoleApp.Commands
{
    #region

    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public class ExitCommand : AbstractCommand
    {
        public ExitCommand(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            this.Engine.HasStarted = false;
        }
    }
}