using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacetime.Core.gRPC
{
    public class GrpcResponse
    {
        public GrpcStatus Status { get; set; }
        public string ResponseBody { get; set; }
        public long ElapsedMs { get; set; }
    }
}
