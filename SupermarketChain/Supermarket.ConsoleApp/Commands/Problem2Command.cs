namespace Supermarket.ConsoleApp.Commands
{
    using Supermarket.ConsoleApp.Interfaces;

    public class Problem2Command : AbstractCommand
    {
        public Problem2Command(IEngine engine)
            :base(engine)
        {
        }

        public override void Execute()
        {
            this.Engine.Output.Append("aaa");
        }
    }

}