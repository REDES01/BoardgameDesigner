using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace BoardgameDesigner;

using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.VisualBasic;

public class Player:SelfAgent
{    
    public string Name { get; set; }
    public string Rule { get; set; }
    public List<string> FeedBacks { get; set; }

    private List<JsonNode> _hand=new List<JsonNode>();
    public Player(string name, string rule = ""):base()
    {
        Name = name;
        Rule = rule;

        FeedBacks = new List<string>();
        
    }
    

    [KernelFunction("draw_a_card")]
    [Description("Draw a card from a deck with a name and add it to your hand.")]
    
    public string DrawACard(string deckName)
    {
        var deck=DecksManager.GetDeck(deckName);
        if (deck != null)
        {
            var card = deck!.DrawTop();
            _hand.Add(card);
            Console.WriteLine(card.ToString());
            return "Successfully drawn from the deck: "+deckName;
        }
        else
        {
            chatHistory.AddUserMessage($"Deck {deckName} not found");
            Console.WriteLine($"Deck {deckName} not found");
        }
        
        return "You just drawed a card.";
    }
    

    [KernelFunction("play_a_card")]
    [Description("Play a card from your hand with its name.")]
    
    public string PlayACard(string cardName)
    {
        JsonNode card = _hand.Find(c=>(string)c["name"]==cardName);
        if (card == null)
        {
            Console.WriteLine($"Card {cardName} not found");
            return $"Card {cardName} not found";
        }
        else
        {
            Console.WriteLine(Name+" played "+(string)card["name"]);
            _hand.Remove(card);
            return $"You played {(string)card["name"]}";

        }
    }
    
    // [KernelFunction("give_feedback")]
    // [Description("Add feedback based on a players previous experience.")]

    // public string AddFeedBack(string feedback)
    // {
    //     FeedBacks.Add(feedback);
    //     throw new NotImplementedException();
    // }
    //
    [KernelFunction("hand_info")]
    [Description("Show what cards are held.")]
    public string ShowHandInfo()
    {
        string handInfo = "";
        foreach (var item in _hand)
        {
            //JsonNode data=JsonNode.Parse(item);
            handInfo += (string)item["name"];
        }
        return handInfo;
    }
}
