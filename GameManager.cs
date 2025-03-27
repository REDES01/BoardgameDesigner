namespace BoardgameDesigner;
using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;

public class GameManager : Agent
{
    public Players Players { get; set; }
    public GameManager() : base()
    {
        // add plugins
        kernel.Plugins.AddFromObject(Players=new Players());
    }

    public ChatHistory ManagerHistory { get => chatHistory; }
    public void AddUserMessage(string s)
    {
        chatHistory.AddUserMessage(s);
    }
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
