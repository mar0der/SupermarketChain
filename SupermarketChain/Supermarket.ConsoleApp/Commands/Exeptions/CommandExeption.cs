namespace Supermarket.ConsoleApp.Commands.Exeptions
{
    #region

    using System;

    #endregion

    public class CommandException : Exception
    {
        public CommandException(string message)
            : base(message)
        {
        }
    }
}