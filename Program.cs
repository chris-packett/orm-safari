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
                Console.WriteLine("(S)pecial commands.");

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
                    case "S":
                        specialPrompts();
                        break;
                    default:
                        Console.WriteLine("Please choose a valid option!");
                        break;
                }

                void addAnimal() 
                {
                    Console.WriteLine("Please tell me what animal you want to add!");
                    var _animalSpecies = Console.ReadLine();

                    //Create a new SeenAnimals instance called newSpecies and assign it a species.
                    var _newSpecies = new SeenAnimals { Species = _animalSpecies };

                    if (_animalSpecies != "")
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

                void displayAnimalsFromLocation()
                {
                    Console.WriteLine("From what location would you like to display all of the animals?");
                    var _location = Console.ReadLine();

                    var animalsToDisplay = db.SeenAnimalsTable.Where(animal => animal.LocationOfLastSeen == _location);

                    Console.WriteLine($"Animals from {_location}");
                    foreach (var _animalSpecies in animalsToDisplay)
                    {
                        Console.WriteLine($"Animal seen: {_animalSpecies.Species}");
                    }
                }

                void removeAnimalsFromLocation()
                {
                    Console.WriteLine("From what location would you like to remove all of the animals?");
                    var _location = Console.ReadLine();

                    var animalsToRemove = db.SeenAnimalsTable.Where(animal => animal.LocationOfLastSeen == _location);

                    foreach (var _animalSpecies in animalsToRemove)
                    {
                        db.SeenAnimalsTable.Remove(_animalSpecies);
                    }

                    db.SaveChanges();
                }

                void getTotalCountSeen()
                {
                    var _totalCount = 0;
                    var _allSeenAnimals = db.SeenAnimalsTable;

                    foreach (var _animal in _allSeenAnimals)
                    {
                        _totalCount += _animal.CountOfTimesSeen;
                    }
                    
                    Console.WriteLine($"You have seen a total of {_totalCount} animals!");
                }

                void getTotalCountSeenWizardsOfOzEdition()
                {
                    var _totalCount = 0;
                    var _allWizardOfOzAnimals = db.SeenAnimalsTable.Where(animal => animal.Species == "Lion" || animal.Species == "Tiger" || animal.Species == "Bear");

                    foreach (var _animal in _allWizardOfOzAnimals)
                    {
                        _totalCount += _animal.CountOfTimesSeen;
                    }

                    Console.WriteLine($"You have seen a total of {_totalCount} animals of Wizard type!");
                }

                void specialPrompts()
                {
                    Console.WriteLine("(D)isplay all animals from a specific location.");
                    Console.WriteLine("(R)emove all animals from a specific location.");
                    Console.WriteLine("(G)et total count of all animals you have seen.");
                    Console.WriteLine("(W)izards of Oz edition: total count of lions, tigers, and bears seen.");

                    var specialUserAction = Console.ReadLine();
                    specialUserAction = specialUserAction.ToUpper();
                    
                    switch (specialUserAction) 
                    {
                        case "D":
                            displayAnimalsFromLocation();
                            break;
                        case "R":
                            removeAnimalsFromLocation();
                            break;
                        case "G":
                            getTotalCountSeen();
                            break;
                        case "W":
                            getTotalCountSeenWizardsOfOzEdition();
                            break;
                        default:
                            Console.WriteLine("Please choose a valid option!");
                            break;
                    }
                }
            }
        }
    }
}
