namespace Spacetime.Core.Shared
{
    public class SpacetimeResponse
    {
        public IEnumerable<HeaderDto> Headers { get; set; }
        public string StatusCode { get; set; }
        public SpacetimeStatus Status { get; set; }
        public string ResponseBody { get; set; }
        public long ElapsedMs { get; set; }
    }
}
