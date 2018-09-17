using System;
using System.Linq;
using orm_safari.models;

namespace orm_safari
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var db = new SafariVacationContext())
            {
                //Prompt and get seen animal from user.
                Console.WriteLine("What is one animal that you have seen today?");
                var species = Console.ReadLine();

                //Create a new SeenAnimals instance called newSpecies and assign it a species.
                var newSpecies = new SeenAnimals { Species = species };

                //Add the new SeenAnimals instance to the SeenAnimalsTable in the SafariVacation database.
                //Technically, we are just adding the newSpecies to a DbSet<SeenAnimals> in C#, which
                //connects to the SQL server and adds a new row into the SeenAnimalsTable.
                db.SeenAnimalsTable.Add(newSpecies);

                //commits changes from C# to the database.
                db.SaveChanges();
            }
        }
    }
}
