using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;


GameManager gameManager = new GameManager();

do
{
    string s = Console.ReadLine()!;
    if (s == "")
    {
        break;
    }
    gameManager.AddUserMessage(s);
    await gameManager.GetReply();

    // logging chat history
    gameManager.SaveHistory("chatHistory.txt");
} while (true);

