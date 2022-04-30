using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Core.Infrastructure
{
    public class SpacetimeRequest
    {
        public int Id { get; set; }
        public SpacetimeType Type { get; set; }
        public SpacetimeStatus Status { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string? RequestBody { get; set; }
        public SpacetimeResponse? Response { get; set; }
        public List<QueryParamDto> QueryParams { get; set; } = new();
        public List<HeaderDto> Headers { get; set; } = new();
        public string Method { get; set; } = "get";
    }
}
