using Microsoft.AspNetCore.Components;

namespace PharmaService.Admin.Client.Common;

public abstract class AbstractPage : ComponentBase
{
    protected bool IsLoading { get; set; }
    protected bool IsProcessing { get; set; }
    
    protected async Task LoadAsync(Func<Task> function)
    {
        IsLoading = true;

        try
        {
            await function();
        }
        finally
        {
            IsLoading = false;
        }
    }

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
}