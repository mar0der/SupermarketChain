namespace Supermarket.ConsoleApp.Commands
{
    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Interfaces;

    public class HelpCommand: AbstractCommand
    {
        public HelpCommand(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            this.Engine.Output.AppendLine(Messages.HelpMenu);
        }
    }
}