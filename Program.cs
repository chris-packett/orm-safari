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
                // //Prompt and get seen animal from user.
                // Console.WriteLine("What is one animal that you have seen today?");
                // var species = Console.ReadLine();

                // //Create a new SeenAnimals instance called newSpecies and assign it a species.
                // var newSpecies = new SeenAnimals { Species = species };

                // if (species == "")
                // {
                //     //If no user input, display message below.
                //     Console.WriteLine("Nothing to add");
                // }
                // else
                // {
                //     //Add the new SeenAnimals instance to the SeenAnimalsTable in the SafariVacation database.
                //     //Technically, we are just adding the newSpecies to a DbSet<SeenAnimals> in C#, which
                //     //connects to the SQL server and adds a new row into the SeenAnimalsTable.
                //     db.SeenAnimalsTable.Add(newSpecies);

                //     //commits changes from C# to the database.
                //     db.SaveChanges();
                // }

                // //Display all of the animals that the user has seen
                // var allSeenAnimals = db.SeenAnimalsTable;

                // foreach (var animal in allSeenAnimals)
                // {
                //     Console.WriteLine($"Animal seen: {animal.Species}");
                // }

                // //Update the CountOfTimesSeen and LocationOfLastSeen for an animal
                // var animalToUpdate = db.SeenAnimalsTable.FirstOrDefault(animal => animal.Id == 4);
                // animalToUpdate.CountOfTimesSeen = 1;
                // animalToUpdate.LocationOfLastSeen = "Grasslands";

                // db.SaveChanges();

                
            }
        }
    }
}
