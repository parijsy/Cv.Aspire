using Microsoft.AspNetCore.Components;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Timers;

namespace Cv.Aspire.Web.Components.Pages.Code_Examples;

public partial class Assistant
{
    [Inject] private Kernel Kernel { get; set; } = null!;
    [Inject] private IChatCompletionService ChatService { get; set; } = null!;

    private string prompt = "";

    private ChatHistory chat = new("When requested, you can change the theme colors of the app using CSS colors.");
    private List<ChatMessage> messages = [];
    private bool showAssistantAlert = false;
    private System.Timers.Timer alertTime = new(5000);

    protected override void OnInitialized()
    {
        alertTime.Elapsed += OnAlertFinish;
        alertTime.AutoReset = false;
    }

    private async Task Submit()
    {
        try
        {
            chat.AddUserMessage(prompt);
            messages.AddUserMessage(prompt);
            prompt = string.Empty;
            StateHasChanged();

            var response = string.Empty;
            var assistantMessage = messages.AddStreamingAssistantMessage();
            OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
            var chunks = ChatService.GetStreamingChatMessageContentsAsync(chat, settings, Kernel);
            await foreach (var chunk in chunks)
            {
                response += chunk;
                assistantMessage.Message += chunk;
                StateHasChanged();
            }

            assistantMessage.IsStreaming = false;
            chat.AddAssistantMessage(response);
            StateHasChanged();
        }
        catch (Exception)
        {
            HandleSubmitException();
        }
    }

    private async Task SubmitQuestion(string question)
    {
        prompt = question;
        await Submit();
    }

    private void HandleSubmitException()
    {
        alertTime.Stop();
        alertTime.Start();
        showAssistantAlert = true;
        messages.RemoveAt(messages.Count - 1);
        StateHasChanged();
    }

    private void OnAlertFinish(object? source, ElapsedEventArgs e)
    {
        InvokeAsync(() =>
        {
            showAssistantAlert = false;
            StateHasChanged();
        });
    }
}

public record ChatMessage
{
    public string Message { get; set; } = string.Empty;
    public AuthorRole Role { get; set; }
    public bool IsStreaming { get; set; }
}

public static class ChatMessageExtensions
{
    public static void AddUserMessage(this List<ChatMessage> messages, string message) =>
        messages.Add(new ChatMessage { Message = message, Role = AuthorRole.User, IsStreaming = false });

    public static ChatMessage AddStreamingAssistantMessage(this List<ChatMessage> messages)
    {
        var message = new ChatMessage { Message = string.Empty, Role = AuthorRole.Assistant, IsStreaming = true };
        messages.Add(message);
        return message;
    }
}