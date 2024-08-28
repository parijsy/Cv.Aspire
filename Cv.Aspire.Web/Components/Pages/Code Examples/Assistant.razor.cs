using Microsoft.AspNetCore.Components;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace Cv.Aspire.Web.Components.Pages.Code_Examples;

public partial class Assistant
{
    [Inject] private Kernel Kernel { get; set; } = null!;
    [Inject] private IChatCompletionService ChatService { get; set; } = null!;

    private string prompt = "";

    private ChatHistory chat = new("When requested, you can change the theme colors of the app using CSS colors.");// Je kan hier de persona mee geven
    // NIET de ChatHistory direct gebruiken voor renderen -> Wordt gevuld met rare shit tijdens function calling

    private async Task Submit()
    {
        var response = string.Empty;
        try
        {
            chat.AddUserMessage(prompt);
            prompt = string.Empty;
            StateHasChanged();

            chat.AddAssistantMessage(response);
            var assistantMessage = chat.Last();
            OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
            var chunks = ChatService.GetStreamingChatMessageContentsAsync(chat, settings, Kernel);
            await foreach (var chunk in chunks)
            {
                assistantMessage.Content += chunk;
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}