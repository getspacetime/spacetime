using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Spacetime.Core.Formatters;

public class JsonFormatter : IFormatter
{
    private readonly ILogger<JsonFormatter> _log;
    private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { WriteIndented = true };

    public JsonFormatter(ILogger<JsonFormatter> log)
    {
        _log = log;
    }

    public string Format(string text)
    {
        try
        {
            _log.LogInformation("Formatting JSON response");

            return JsonSerializer.Serialize(
                JsonSerializer.Deserialize<object>(text), _jsonOptions);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Could not format JSON response");
            return text;
        }
    }
}