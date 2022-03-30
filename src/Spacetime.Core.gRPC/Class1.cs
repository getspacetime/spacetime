using Spacetime.gRPC.Wrapper;

namespace Spacetime.Core.gRPC
{
    public class GrpcHelper
    {
        public void Test(string importPath, string protoFileName)
        {
            var curl = new GRPCurl();
            var svcs = curl.ListServices(importPath, protoFileName);
            var methods = curl.ListMethods(importPath, protoFileName);

        }
    }
}