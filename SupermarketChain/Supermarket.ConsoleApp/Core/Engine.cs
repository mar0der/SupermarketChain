namespace Supermarket.CnsoleApp.Core
{
    using System;
    using System.Data;

    public class Engine
    {
        public Engine()
        {
            
        }
        public void Run()
        {
            while (true)
            {
                Console.Write("Enter command:");
                var commandString = Console.ReadLine();
                if (commandString == "exit")
                {
                    break;
                }
                var parameters = this.CommandParser(commandString);
                var output = this.CommandExecute(parameters);
                Console.WriteLine(output);
            }
        }

        private string[] CommandParser(string commandString)
        {
            string[] parameters = new[] { "dd"};
            
            return parameters;
        }
        private string CommandExecute(string[] Parameters)
        {
            return "output";
        }


    }
}
