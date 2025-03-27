namespace BoardgameDesigner;

using System.ComponentModel;
using Microsoft.SemanticKernel;
using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;

public class Players:Agent
{

    private static List<Player> players = new List<Player>();

    // builder for players


    [KernelFunction("create_player")]
    [Description("Create a new player that can play the game and give feedbacks.")]
    public static string CreatePlayer(string name)
    {
        var player = new Player(name);
        players.Add(player);
        return "Player created.";
    }

    [KernelFunction("find_player")]
    [Description("Find if player with certain name exist.")]
    public static string GetPlayerInfo(string name)
    {
        var player = players.Find(p => p.Name == name);
        if (player == null)
        {
            return "Player not found.";
        }
        return "Player found.";
    }
}
