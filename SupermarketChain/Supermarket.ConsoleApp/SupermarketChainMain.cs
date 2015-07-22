namespace Supermarket.ConsoleApp
{
    #region

    using Supermarket.ConsoleApp.CoreLogic;

    #endregion

    internal class SupermarketChainMain
    {
        private static void Main()
        {
            var engine = new Engine();
            engine.Run();
        }
    }
}