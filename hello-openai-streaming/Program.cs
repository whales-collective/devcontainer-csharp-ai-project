using OpenAI;
using OpenAI.Chat;
using System.ClientModel;

ChatClient client = new(
    model: Environment.GetEnvironmentVariable("MODEL_RUNNER_CHAT_MODEL") 
        ?? throw new InvalidOperationException("MODEL_RUNNER_CHAT_MODEL env var not set"),
    credential: new ApiKeyCredential("I<3DockerModelRunner"),
    options: new OpenAIClientOptions()
    {
        Endpoint = new Uri(Environment.GetEnvironmentVariable("MODEL_RUNNER_BASE_URL") 
            ?? throw new InvalidOperationException("MODEL_RUNNER_BASE_URL env var not set"))
    }
);

CollectionResult<StreamingChatCompletionUpdate> completionUpdates = client.CompleteChatStreaming("What is the best pizza of the world?");


Console.Write($"[ASSISTANT]: ");
foreach (StreamingChatCompletionUpdate completionUpdate in completionUpdates)
{
    if (completionUpdate.ContentUpdate.Count > 0)
    {
        Console.Write(completionUpdate.ContentUpdate[0].Text);
    }
}
