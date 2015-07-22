namespace Supermarket.ConsoleApp.Factories
{
    #region

    using System;
    using System.Linq;
    using System.Reflection;

    using Supermarket.ConsoleApp.Commands;
    using Supermarket.ConsoleApp.Interfaces;

    #endregion

    public class CommandFactory
    {
        private const string CommandSuffix = "Command";

        public static IExecutable Create(string commandInput, IEngine engine)
        {
            var data = commandInput.Split(null);
            var commandName = data[0].ToLower();

            var commandClass =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.IsClass && t.Namespace == "Supermarket.ConsoleApp.Commands")
                    .Where(t => t.Name.EndsWith(CommandSuffix))
                    .First(t => t.Name.Replace(CommandSuffix, string.Empty).ToLower().Equals(commandName));

            var command = Activator.CreateInstance(commandClass, engine) as AbstractCommand;

            foreach (var field in data)
            {
                command.Data.Add(field);
            }

            return command;
        }
    }
}