using DeckDiff;
using System.CommandLine;
using System.IO;

namespace DeckDiff;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var oldDeckOption = new Option<FileInfo?>(
                  name: "--old-deck-file",
                  description: "The file containing the old deck list");

        var newDeckOption = new Option<FileInfo?>(
                  name: "--new-deck-file",
                  description: "The file containing the new deck list");

        var rootCommand = new RootCommand(
            @"Comparing two MTG decks to find the cards to remove from the 
            old deck and to add from the new one.");

        rootCommand.AddOption(oldDeckOption);
        rootCommand.AddOption(newDeckOption);

        string? oldDeck = "";
        string? newDeck = "";

        rootCommand.SetHandler((oldDeckFile, newDeckFile) =>
            {
                if (oldDeckFile is not null)
                {
                    oldDeck = File.ReadAllText(oldDeckFile.FullName);
                    Console.WriteLine(oldDeck);
                }
                if (newDeckFile is not null)
                {
                    newDeck = File.ReadAllText(newDeckFile.FullName);
                    Console.WriteLine(newDeck);
                }
            },
            oldDeckOption, newDeckOption);

        Console.WriteLine(oldDeck);
        Console.WriteLine(newDeck);

        return await rootCommand.InvokeAsync(args);
    }
}


