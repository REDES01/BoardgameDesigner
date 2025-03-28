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
    public DecksManager(){}
    private static readonly List<Deck> _decks=new List<Deck>();
    public int DeckCount => _decks.Count;
    public static string DeckNames => String.Join(",",_decks.Select(d=>d.Name));

    [KernelFunction("create_deck")]
    [Description("Load a json file and create a new deck of cards and add it to the decks.")]

    public static void CreateDeck(string deckName="sample")
    {
           string filepath = FileSystem.AbsoluteFolderPath+"/storage/"+deckName+".json";
           string filecontent = File.ReadAllText(filepath);
           // Console.WriteLine(filecontent);
           var cards=JsonSerializer.Deserialize<List<dynamic>>(filecontent)!;
           // Console.WriteLine(_deck.Count);
           // Console.WriteLine(_deck);
           Deck deck=new Deck(deckName,cards);
           _decks.Add(deck);
           Console.WriteLine($"Deck loaded: {deckName}");
           Console.WriteLine($"Deck count: {_decks.Count}");
           Console.WriteLine($"Deck names: {DeckNames}");
    }
}