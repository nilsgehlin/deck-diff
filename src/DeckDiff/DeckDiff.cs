using System;
using System.Collections.Generic;
using System.Linq;

namespace DeckDiff;

internal record Card(string Name, int Qty);

public class Deck
{
    public static (string toRemove, string toAdd) Compare(string oldDeck, string newDeck)
    {
        var oldCards = ParseDeck(oldDeck);
        var newCards = ParseDeck(newDeck);
        var toRemove = CardNotSharedWith(oldCards, newCards);
        var toAdd = CardNotSharedWith(newCards, oldCards);
        return (RenderCardList(toRemove), RenderCardList(toAdd));
    }

    private static Card LineToCard(string line)
    {
        var split = line.Split(" ");
        var qty = int.Parse(split[0]);
        var name = string.Join(" ", split.Skip(1));
        return new Card(name, qty);
    }

    private static string RenderCardList(IEnumerable<Card> cardList)
    {
        return string.Join(Environment.NewLine, cardList.Select(card => $"{card.Qty} {card.Name}"));
    }

    private static List<Card> ParseDeck(string deck)
    {
        static bool isNotComment(string line) => line.Substring(0, 2) != "//";
        static bool isNotEmpty(string line) => line != "";
        return deck
            .Split(Environment.NewLine)
            .Select(line => line.Trim())
            .Where(line => isNotEmpty(line) && isNotComment(line))
            .Select(LineToCard)
            .ToList();

    }

    private static IEnumerable<Card> CardNotSharedWith(List<Card> deck1, List<Card> deck2)
    {
        foreach (var card in deck1)
        {
            var searchResult = deck2.Find(newCard => card.Name == newCard.Name);
            if (searchResult is null)
            {
                yield return card;
            }
            else if (card.Qty > searchResult.Qty)
            {
                yield return card with { Qty = card.Qty - searchResult.Qty };
            }
        }
    }

}
