using System;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

namespace DeckDiff;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var oldDeckOption = new Option<FileInfo?>(
                  name: "--old-deck-file",
                  description: "The file containing the old deck list");

        var newDeckOption = new Option<FileInfo?>(
                  name: "--new-deck-file",
                  description: "The file containing the new deck list");

        var outputDirOption = new Option<DirectoryInfo?>(
                  name: "--output-dir",
                  description: "Director to place the output files in");

        var rootCommand = new RootCommand(
            @"Comparing two MTG decks to find the cards to remove from the 
            old deck and to add from the new one.");

        rootCommand.AddOption(oldDeckOption);
        rootCommand.AddOption(newDeckOption);
        rootCommand.AddOption(outputDirOption);

        rootCommand.SetHandler((oldDeckFile, newDeckFile, outputDir) =>
            {
                if (oldDeckFile is not null && newDeckFile is not null && outputDir is not null)
                {
                    var oldDeck = File.ReadAllText(oldDeckFile.FullName);
                    var newDeck = File.ReadAllText(newDeckFile.FullName);

                    (string toRemove, string toAdd) = Deck.Compare(oldDeck, newDeck);
                    Console.WriteLine(outputDir.FullName);

                    File.WriteAllText($"{outputDir.FullName}/{Path.GetFileNameWithoutExtension(oldDeckFile.Name)}-remove.txt", toRemove);
                    File.WriteAllText($"{outputDir.FullName}/{Path.GetFileNameWithoutExtension(newDeckFile.Name)}-add.txt", toAdd);
                }
            },
            oldDeckOption, newDeckOption, outputDirOption);

        return await rootCommand.InvokeAsync(args);
    }
}


