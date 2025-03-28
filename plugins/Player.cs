namespace BoardgameDesigner;

using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.VisualBasic;

public class Player:Agent
{    public string Name { get; set; }
    public string Rule { get; set; }
    public List<string> FeedBacks { get; set; }

    public Player(string name, string rule = "")
    {
        Name = name;
        Rule = rule;

        FeedBacks = new List<string>();
    }
    
    // todo draw a card
    [KernelFunction("draw_a_card")]
    [Description("Draw a card from a deck and add it to your hand.")]

    public void DrawACard(string deckName)
    {
        
    }
    
    // todo play a card
    [KernelFunction("play_a_card")]
    [Description("Play a card from your hand.")]

    public void PlayACard(string deckName)
    {
        
    }
    
    [KernelFunction("give_feedback")]
    [Description("Add feedback based on a players previous experience.")]

    public void AddFeedBack(string feedback)
    {
        FeedBacks.Add(feedback);
    }

}
