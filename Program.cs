﻿using FountainOfObjects;
using FountainOfObjects.Entities;
using FountainOfObjects.GameLogic;

//Console.WriteLine("Welcome to the Fountain Of Objects!");

DateTime gameStart;

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
    gameStart = DateTime.Now;

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

    // Player has died
    if (!Player.isAlive)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("You have walked into a trap and died.");
        DisplayTimeTaken();
        Console.WriteLine("Press enter to try again.");
        Console.ResetColor();
        Console.ReadLine();
    }

    // Player has escaped
    if (Player.hasEscaped)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The Fountain of Objects has been reactivated and you have escaped with your life!");
        DisplayTimeTaken();
        Console.WriteLine("Press enter to play again.");
        Console.ResetColor();
        Console.ReadLine();
    }
}

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

void DisplayTimeTaken()
{
    TimeSpan timeTaken = DateTime.Now - gameStart;

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"You were in the cavern for: {timeTaken.Hours} hours, {timeTaken.Minutes} minutes and {timeTaken.Seconds} seconds.");
    Console.ResetColor();
}
