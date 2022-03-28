using Spacetime.gRPC.Wrapper;

namespace Spacetime.Core.gRPC
{
    public class Class1
    {
        public void Test(string importPath, string protoFileName)
        {
            var curl = new GRPCurl();
            var methods = curl.ListMethods(importPath, protoFileName);

        }
    }
}