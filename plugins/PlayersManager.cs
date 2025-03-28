namespace BoardgameDesigner;

using System.ComponentModel;
using Microsoft.SemanticKernel;
using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;

/// <summary>
/// Manages all players
/// 
/// </summary>
public class PlayersManager:Agent
{
    private static readonly List<Player> _players = new List<Player>();

    public int PlayerCount { get=>_players.Count; }
    // builder for players
    

    [KernelFunction("create_player")]
    [Description("Create a new player with distinct name.")]
    public static string CreatePlayer(string name)
    {
        var player = _players.Find(p => p.Name == name);
        if (player == null)
        {
            _players.Add(new Player(name));
            return "Player created.";
        }
        return "Player already exists.";
    }

    [KernelFunction("find_player")]
    [Description("Find if player with certain name exist.")]
    public static string GetPlayerInfo(string name)
    {
        var player = _players.Find(p => p.Name == name);
        if (player == null)
        {
            return "Player not found.";
        }
        return "Player found.";
    }

    public Player GetPlayer(string name)
    {
        return _players.Find(p => p.Name == name)!;
    }
}
