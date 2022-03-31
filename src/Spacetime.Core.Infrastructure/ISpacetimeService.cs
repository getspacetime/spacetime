namespace Spacetime.Core
{
    public interface ISpacetimeService
    {
        Task<SpacetimeResponse> Execute(SpacetimeRequest request);
    }
}
