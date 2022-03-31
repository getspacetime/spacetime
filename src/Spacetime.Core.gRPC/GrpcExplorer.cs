using Spacetime.gRPC.Wrapper;

namespace Spacetime.Core.gRPC
{
    public class GrpcExplorer : IGrpcExplorer
    {
        public GrpcExploreResult GetExplorer(string importPath, string protoFileName)
        {
            var explorer = new GrpcExploreResult();
            explorer.Services = ListServices(importPath, protoFileName).Select(p => new GrpcServiceDefinition { Name = p}).ToList();
            foreach (var svc in explorer.Services)
            {
                svc.Methods = ListMethods(importPath, protoFileName, svc.Name).Select(p => new GrpcMethodDefinition { Name = p}).ToList();
            }

            return explorer;
        }
        
        public IEnumerable<string> ListServices(string importPath, string protoFileName)
        {
            var curl = new GRPCurl();
            var result = curl.ListServices(importPath, protoFileName);
            return result.Items.Select(p => p.Name);
        }

        public IEnumerable<string> ListMethods(string importPath, string protoFileName, string svc)
        {
            var curl = new GRPCurl();
            var result = curl.ListMethods(importPath, protoFileName, svc);
            return result.Items.Select(p => p.Name);
        }
    }
}