using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Cv.Aspire.Web.Code.SemanticKernel.Plugins;

public class TimePlugin
{
    [KernelFunction]
    [Description("Get the current time")]
    public DateTime SetThemeColors()
    {
        return DateTime.Now;
    }
}