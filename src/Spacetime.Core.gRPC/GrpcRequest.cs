using Spacetime.Core.Shared;

namespace Spacetime.Core.gRPC;

public class GrpcRequest
{
    public Guid Id { get; set; }
    public Guid ServiceDefinitionId { get; set; }
    public Guid MethodDefinitionId { get; set; }
    public string URL { get; set; }
    public string Body { get; set; }
    public SpacetimeResponse Response { get; set; } = new();
}