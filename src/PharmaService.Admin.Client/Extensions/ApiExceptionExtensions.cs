using System.Text.Json;
using Refit;

namespace PharmaService.Admin.Client.Extensions;

public static class ApiExceptionExtensions
{
    public static string GetErrorDetails(this ApiException? exception)
    {
        if (exception is null || string.IsNullOrEmpty(exception.Content))
            return "Что-то пошло не так";

        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        if (problemDetails is null)
        {
            return $@"<div>
                <h3>INTERNAL_SERVER_ERROR</h3>
                <p>{exception.Content}</p>
            </div>";
        }

        var errorCode = "INTERNAL_SERVER_ERROR";
        if (problemDetails.Extensions.TryGetValue("error", out var error))
        {
            errorCode = error.ToString()?.ToUpper();
        }

        return $@"<div>
                <h3>{errorCode}</h3>
                <p>{problemDetails.Detail}</p>
            </div>";
    }
}