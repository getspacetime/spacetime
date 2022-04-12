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
        public string Name { get; set; }
        public string URL { get; set; }
        public string? RequestBody { get; set; }
        public SpacetimeResponse? Response { get; set; }
        public List<QueryParamDto> QueryParams { get; set; } = new List<QueryParamDto>();
        public List<HeaderDto> Headers { get; set; } = new List<HeaderDto>();
        public string Method { get; set; } = "get";
        
        public string GetName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return URL;
            }

            return Name;
        }

        // grpc fields
        public string ImportPath { get; set; }
        public string ProtoFile { get; set; } = "greet.proto";
    }

    public enum SpacetimeType
    {
        NotSet,
        REST,
        gRPC,
        WebSockets
    }
}
