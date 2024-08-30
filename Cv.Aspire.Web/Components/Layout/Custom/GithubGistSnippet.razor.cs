using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;

namespace Cv.Aspire.Web.Components.Layout.Custom;

// Based on https://stackoverflow.com/a/70749777
public partial class GithubGistSnippetBase : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = default!;

    [Parameter, EditorRequired] public string Title { get; set; } = default!;
    [Parameter, EditorRequired] public string UserId { get; set; } = default!;
    [Parameter, EditorRequired] public string FileName { get; set; } = default!;

    protected string stylesheet = string.Empty;
    protected string snippet = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        var result = await HttpClient.GetAsync($"https://gist.github.com/{UserId}/{FileName}");
        var text = await result.Content.ReadAsStringAsync();

        // The html is written inbetween a document.write(''), so we pick up the content and write it directly.
        // The first document.write will contain the stylesheet, and the second the snippet
        var regex = DocumentWriteContentRegex();

        var matches = regex.Matches(text);
        stylesheet = matches[0].Groups[0].Value;
        snippet = Regex.Unescape(matches[1].Groups[0].Value);
    }

    [GeneratedRegex("(?<=document.write\\(')(.*)(?='\\))")]
    internal static partial Regex DocumentWriteContentRegex();
}