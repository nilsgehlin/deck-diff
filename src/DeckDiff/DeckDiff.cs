using System;

namespace DeckDiff;

public record Card(string Name, int Qty);

public class CompareDecks
{
    public static Card LineToCard(string line)
    {
        var split = line.Split(" ");
        var qty = Int32.Parse(split[0]);
        var name = String.Join(" ", split.Skip(1));
        return new Card(name, qty);
    }

    public static string RenderCardList(IEnumerable<Card> cardList)
    {
        return String.Join(Environment.NewLine, cardList.Select(card => $"{card.Qty} {card.Name}"));
    }

    public static List<Card> ParseDeck(string deck)
    {
        static bool isNotComment(string line) => line.Substring(0, 2) != "//";
        return deck
            .Split(Environment.NewLine)
            .Select(line => line.Trim())
            .Where(isNotComment)
            .Select(LineToCard)
            .ToList();
    }

    public static List<Card> GetCardsToRemove(List<Card> oldCards, List<Card> newCards)
    {
        var toRemove = new List<Card>();
        foreach (var oldCard in oldCards)
        {
            var searchResult = newCards.Find(newCard => oldCard.Name == newCard.Name);
            if (searchResult is null)
            {
                toRemove.Add(oldCard);
            }
            else
            {
                if (oldCard.Qty > searchResult.Qty)
                {
                   toRemove.Add(oldCard with {Qty = oldCard.Qty - searchResult.Qty}); 
                }
            }
        }
        return toRemove;
    }

    public static (string toRemove, string toAdd) Compare(string oldDeck, string newDeck)
    {
        var oldCards = ParseDeck(oldDeck);
        var newCards = ParseDeck(newDeck);
        var toRemove = GetCardsToRemove(oldCards, newCards);
        var toAdd = GetCardsToRemove(newCards, oldCards);

        return (RenderCardList(toRemove), RenderCardList(toAdd));
    }
}
