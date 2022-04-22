using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spacetime.Helpers
{
    public class HtmlOffset
    {
        public double Top { get; set; }
        public double Left { get; set; }
    }
    public class ScriptUtils : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ScriptUtils(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./utils.js").AsTask());
        }

        public async ValueTask<string> Log(string message, object args = null)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("log", message, args);
        }

        public async ValueTask<HtmlOffset> GetOffset(ElementReference element)
        {
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
