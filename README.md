# .Net IDE + Docker Model Runner + OpenAI SDK Example

This example demonstrates how to use the OpenAI SDK in a .NET console application to interact with a Docker Model Runner instance, using devcontainers for the development environment.

## Hello Demo: Chat completion

### Generate the project

```bash
dotnet new console -n hello-openai
cd hello-openai
dotnet add package OpenAI
```

### Add the code

Replace the content of `Program.cs` with the following code:

```csharp
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

ChatCompletion completion = client.CompleteChat("What is the best pizza of the world?");

Console.WriteLine($"[ASSISTANT]: {completion.Content[0].Text}");
```

### Run the project

```bash
dotnet run
```

## Hello Demo: Chat completion with streaming

### Generate the project

```bash
dotnet new console -n hello-openai-streaming
cd hello-openai-streaming
dotnet add package OpenAI
```

### Add the code

Replace the content of `Program.cs` with the following code:

```csharp
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

```

### Run the project

```bash
dotnet run
```