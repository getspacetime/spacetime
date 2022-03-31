namespace Spacetime.Core.gRPC
{
    public interface IGrpcExplorer
    {
        GrpcExploreResult GetExplorer(string importPath, string protoFileName);
    }
}