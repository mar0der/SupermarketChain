namespace Supermarket.ConsoleApp.CoreLogic
{
    #region

    using System;
    using System.Text;

    using Supermarket.ConsoleApp.Commands.Exeptions;
    using Supermarket.ConsoleApp.Constants;
    using Supermarket.ConsoleApp.Factories;
    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public class Engine : IEngine
    {
        public Engine()
        {
            this.HasStarted = true;
            this.Output = new StringBuilder();
        }
        public bool HasStarted { get; set; }

        public StringBuilder Output { get; private set; }

        public virtual void Run()
        {
            while (this.HasStarted)
            {
                Console.Write("Enter command, help or exit:");
                this.ExecuteCommandLoop();
            }
        }

        private void ExecuteCommandLoop()
        {

            this.Output.Clear();
            var inputCommand = Console.ReadLine();

            try
            {
                var command = CommandFactory.Create(inputCommand, this);
                command.Execute();
            }
            catch (CommandException ex)
            {
                this.Output.AppendLine(ex.Message);
            }
            catch (InvalidOperationException exception)
            {
                this.Output.AppendLine(Messages.InvalidCommand);
                this.Output.AppendLine(exception.Message);
            }

            Console.Write(this.Output);
        }
    }
}