using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PharmaService.Admin.Client.Common;

public abstract class AbstractDialog : ComponentBase
{
    [CascadingParameter]
    protected MudDialogInstance MudDialog { get; set; } = null!;
    
    protected bool IsProcessing { get; set; }
    
    protected async Task ProcessAsync(Func<Task> function)
    {
        IsProcessing = true;

        try
        {
            await function();
        }
        finally
        {
            IsProcessing = false;
        }
    }
    
    protected void CloseDialog() => MudDialog.Cancel();
}