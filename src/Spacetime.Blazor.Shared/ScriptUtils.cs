using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Spacetime.Blazor.Shared
{
    public class ScriptUtils : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly ILogger<ScriptUtils> _log;
        public ScriptUtils(ILogger<ScriptUtils> log, IJSRuntime jsRuntime)
        {
            _log = log;
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./utils.js").AsTask());
        }

        public async ValueTask<string> Log(string message, object args = null)
        {
            _log.LogInformation($"console.log: {message}");
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("log", message, args);
        }

        public async ValueTask<bool> CopyToClipboard(string contents)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("copyToClipboard", contents);
        }

        public async ValueTask<HtmlOffset> GetOffset(ElementReference element)
        {
            _log.LogInformation("Calculating offset for element {element}", element);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var module = await moduleTask.Value;
            var o = await module.InvokeAsync<object>("getOffset", element);
            var json = JsonSerializer.Serialize(o, options);
            return JsonSerializer.Deserialize<HtmlOffset>(json, options);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
