using System;
using TeamBuilder.Data;

namespace TeamBuilder.App
{
  public class Application
    {
        static void Main(string[] args)
        {
         // DatabaseInitialize();

            Engine engine=new Engine(new CommandDispatcher());
            engine.Run();
        }

        public static void DatabaseInitialize()
        {
            var db = new TeamBuilderContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Console.WriteLine("Database created successfully!");
            Console.WriteLine("Please enter a command:");
        }
    }
}
