namespace Supermarket.ConsoleApp.Commands
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using MysqlDbFirst;

    using Supermarket.ConsoleApp.Interfaces;


    public class Problem7Command:AbstractCommand
    {
        public Problem7Command(IEngine engine)
            : base(engine)
        {
        }

        public override void Execute()
        {
            //his should transfer the data holding the vendors, their products, their incomes by product and their expenses. Th
            Console.WriteLine("boo");
            using (var db = new supermarket_chainEntities())
            {

                Console.WriteLine(db.ToString());
                Console.WriteLine(db.expenses.Count());
                var vendor = new MysqlDbFirst.vendor()
                { vendor_name = "gosho" };
                var expense = new MysqlDbFirst.expens()
                                  {
                                      amount = 10.0m,
                                      vendor = vendor
                                  };
                db.expenses.Add(expense);
                db.SaveChanges();
                
            }
        }
    }
}