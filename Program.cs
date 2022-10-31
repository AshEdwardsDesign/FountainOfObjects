using FountainOfObjects;
using FountainOfObjects.Entities;
using FountainOfObjects.GameLogic;

//Console.WriteLine("Welcome to the Fountain Of Objects!");

while (true)
{
    StartGame();
}

void StartGame()
{
    Cavern.BuildCavern();
    Player.StartOfNewGame();
    PlayGame();
}

void PlayGame()
{
    while (Player.isAlive && !Player.hasEscaped)
    {
        Console.Clear();
        DisplayTitle();
        Cavern.DisplayCavern();
        Console.WriteLine();
        Cavern.SenseNearbyEntities(Player.PlayerPosition);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("What would you like to do? ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Options are: north, south, east, or west");
        Console.ResetColor();
        Console.WriteLine();
        Console.Write("Your choice: ");
        string input = Console.ReadLine() ?? "";

        UserInput.ParseInput(input);
    }
}

Console.ReadLine();

void DisplayTitle()
{
    string title = "|| THE FOUNTAIN OF OBJECTS ||";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine(new String('|', title.Length));
    Console.WriteLine(title);
    Console.WriteLine(new String('|', title.Length));
    Console.ResetColor();
    Console.WriteLine();
}
