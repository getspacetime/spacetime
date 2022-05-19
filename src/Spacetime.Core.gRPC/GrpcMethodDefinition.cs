namespace Spacetime.Core.gRPC
{
    public class GrpcMethodDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }

        /// <value>
        /// Indicates if client streams multiple requests.
        /// </value>
        public bool IsClientStreaming { get; set; }

        /// <value>
        /// Indicates if server streams multiple responses.
        /// </value>
        public bool IsServerStreaming { get; set; }
    }

}