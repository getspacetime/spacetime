using Spacetime.Core.Infrastructure;

namespace Spacetime.Core.Services
{
    /// <summary>
    /// Manages the stored protobuf services the user has saved.
    /// </summary>
    public interface IProtobufService
    {
        Task<IEnumerable<GrpcServiceDefinition>> GetServiceDefinitions();
        Task Save(List<GrpcServiceDefinition> services);
    }
}
