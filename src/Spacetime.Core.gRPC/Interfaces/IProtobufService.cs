namespace Spacetime.Core.gRPC.Interfaces
{
    /// <summary>
    /// Manages the stored protobuf services the user has saved.
    /// </summary>
    public interface IProtobufService
    {
        Task<IEnumerable<GrpcServiceDefinition>> GetServiceDefinitions();
        Task Save(List<GrpcServiceDefinition> services);
        Task Remove(int serviceId);
    }
}
