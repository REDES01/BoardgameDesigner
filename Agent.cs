namespace BoardgameDesigner;
using BoardgameDesigner;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.IO;

/// <summary>
/// The agent class is the base class for all agents in the system.
/// It provides the basic functionality for interacting with the chat completion service.
/// The agent class is responsible for creating the kernel and chat completion service, and managing the chat history.
/// The agent class also provides a method for getting a reply from the chat completion service and adds the reply to the chat history.
/// </summary>
public class Agent
{

    string modelId = "gpt-4o-2024-08-06"!;
    string openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")!;
    string orgId = "org-pmvdpJyLN5zbWaVP1dqQYpEY";


    protected IKernelBuilder builder = Kernel.CreateBuilder();

    // Build the kernel
    protected Kernel kernel;
    protected IChatCompletionService chatCompletionService;

    // Create a chat history object
    protected ChatHistory chatHistory = [];

    protected OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings
    {
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
    };
    public Agent()
    {
        builder.AddOpenAIChatCompletion(modelId, openAiApiKey, orgId);
        kernel = builder.Build();
        chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
    }

    public void AddUserMessage(string s)
    {
        chatHistory.AddUserMessage(s);
    }
    public virtual async Task GetReply()
    {
        ChatMessageContent reply = await chatCompletionService.GetChatMessageContentAsync(
            chatHistory,
            kernel: kernel,
            executionSettings: openAIPromptExecutionSettings
        );
        chatHistory.AddAssistantMessage(reply.ToString());
    }
    
    public void SaveHistory(string filename)
    {
        using (StreamWriter sw = new StreamWriter(File.Open(FileSystem.AbsoluteFolderPath+"/prompt_history/"+filename, FileMode.OpenOrCreate)))
        {
            foreach (var message in chatHistory)
            {
                sw.WriteLine(message.Role + ": " + message.Content);
            }
        }
    }
}