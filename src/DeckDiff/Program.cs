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

        rootCommand.SetHandler((oldDeckFile, newDeckFile) =>
            {
                if (oldDeckFile is not null && newDeckFile is not null)
                {
                    var oldDeck = File.ReadAllText(oldDeckFile.FullName);
                    var newDeck = File.ReadAllText(newDeckFile.FullName);

                    (string toRemove, string toAdd) = Deck.Compare(oldDeck, newDeck);

                    File.WriteAllText($"./{Path.GetFileNameWithoutExtension(oldDeckFile.Name)}-remove.txt", toRemove);
                    File.WriteAllText($"./{Path.GetFileNameWithoutExtension(newDeckFile.Name)}-add.txt", toAdd);
                }
            },
            oldDeckOption, newDeckOption);

        return await rootCommand.InvokeAsync(args);
    }
}


