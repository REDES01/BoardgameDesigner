using System.Text.Json;
using System.Text.Json.Serialization;

namespace BoardgameDesigner;
using System.ComponentModel;
using Microsoft.SemanticKernel;

/// <summary>
/// represents a single deck
/// </summary>
/// <param name="name">name of the deck</param>
/// <param name="cards">cards in the deck</param>
public class Deck(string name, List<dynamic> cards)
{
    public string Name { get; init; } = name;
    private List<dynamic> _cards = cards;
    
    public void AddToTopCard(dynamic card)
    {
        _cards.Add(card);
    }

    public dynamic DrawTop()
    {
        var card=_cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return card;
    }
}

/// <summary>
/// decks manager
/// </summary>
public class DecksManager
{

    private static readonly List<Deck?> Decks=new List<Deck?>();

    public int DeckCount => Decks.Count;
    public static string DeckNames => String.Join(",",Decks.Select(d=>d.Name));

    [KernelFunction("create_deck")]
    [Description("Load a json file and create a new deck of cards and add it to the decks.")]

    public static void CreateDeck(string deckName="example")
    {
           string filepath = FileSystem.AbsoluteFolderPath+"/storage/"+deckName+".json";
           string filecontent = File.ReadAllText(filepath);
           // Console.WriteLine(filecontent);
           var cards=JsonSerializer.Deserialize<List<dynamic>>(filecontent)!;
           // Console.WriteLine(_deck.Count);
           // Console.WriteLine(_deck);
           Deck? deck=new Deck(deckName,cards);
           Decks.Add(deck);
           Console.WriteLine($"Deck loaded: {deckName}");
           Console.WriteLine($"Deck count: {Decks.Count}");
           Console.WriteLine($"Deck names: {DeckNames}");
    }

    // get a deck by name
    public static Deck? GetDeck(string name)
    {
        return Decks.Find(d => d!.Name == name);
    }
}