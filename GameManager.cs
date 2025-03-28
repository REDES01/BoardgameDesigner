namespace BoardgameDesigner;
using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;

public class GameManager : Agent
{

    public PlayersManager PlayersManager { get; init; }
    public GameManager() : base()
    {
        // add plugins
        kernel.Plugins.AddFromObject(PlayersManager=new PlayersManager());
        kernel.Plugins.AddFromObject(new DecksManager());
    }

    public ChatHistory ManagerHistory => chatHistory;


    public override async Task GetReply()
    {
        ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
            chatHistory,
            kernel: kernel,
            executionSettings: openAIPromptExecutionSettings
        );
        Console.WriteLine(reply.ToString());
        chatHistory.AddAssistantMessage(reply.ToString());
    }
}
