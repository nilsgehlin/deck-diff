using DeckDiff;
using System.CommandLine;

namespace DeckDiff;

class Program
{
    static int Main(string[] args)
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

        rootCommand.SetHandler((file) =>
            {
                ReadFile(file!);
            },
            oldDeckOption);

        return await rootCommand.InvokeAsync(args);
    }
}


