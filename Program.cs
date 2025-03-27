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
    using (StreamWriter sw = new StreamWriter(File.Open("./prompt_history/chatHistory.txt", FileMode.Truncate)))
    {
        foreach (var message in gameManager.ManagerHistory)
        {
            sw.WriteLine(message.Role + ": " + message.Content);
        }
    }

} while (true);

