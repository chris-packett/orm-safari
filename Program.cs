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
                //Prompt user for what they want to do to their Safari Collection.
                Console.WriteLine("(A)dd an animal.");
                Console.WriteLine("(D)isplay all animals from the Safari thus far.");
                Console.WriteLine("(U)pdate an animal.");

                var userAction = Console.ReadLine();
                userAction = userAction.ToUpper();
                
                switch (userAction) 
                {
                    case "A":
                        addAnimal();
                        break;
                    case "D":
                        displayAnimals();
                        break;
                    case "U":
                        updateAnimal();
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option!");
                        break;
                }

                void addAnimal() 
                {
                    Console.WriteLine("Please tell me what animal you want to add!");
                    var _species = Console.ReadLine();

                    //Create a new SeenAnimals instance called newSpecies and assign it a species.
                    var _newSpecies = new SeenAnimals { Species = _species };

                    if (_species != "")
                    {
                        //Add the new SeenAnimals instance to the SeenAnimalsTable in the SafariVacation database.
                        //Technically, we are just adding the newSpecies to a DbSet<SeenAnimals> in C#, which
                        //connects to the SQL server and adds a new row into the SeenAnimalsTable.
                        db.SeenAnimalsTable.Add(_newSpecies);

                        //commits changes from C# to the database.
                        db.SaveChanges();
                    }
                    else
                    {
                        //If no user input, display message below.
                        Console.WriteLine("Nothing to add");
                    }
                }

                void displayAnimals()
                {
                    //Display all of the animals that the user has seen
                    var _allSeenAnimals = db.SeenAnimalsTable;

                    foreach (var _animal in _allSeenAnimals)
                    {
                        Console.WriteLine($"Animal seen: {_animal.Species}");
                    }
                }

                void updateAnimal()
                {
                    Console.WriteLine("Please tell me what animal you want to update!");
                    var _animalSpecies = Console.ReadLine();
                    Console.WriteLine("How many times did you see this animal?");
                    var _animalSeenCount = Console.ReadLine();
                    Console.WriteLine("Where did you last see this animal?");
                    var _animalLocationSeen = Console.ReadLine();

                    //Update the CountOfTimesSeen and LocationOfLastSeen for an animal
                    var animalToUpdate = db.SeenAnimalsTable.FirstOrDefault(animal => animal.Species == _animalSpecies);

                    //Update properties of the selected animal.
                    animalToUpdate.CountOfTimesSeen = int.Parse(_animalSeenCount);
                    animalToUpdate.LocationOfLastSeen = _animalLocationSeen;

                    //commits changes from C# to the database.
                    db.SaveChanges();
                }
            }
        }
    }
}
