namespace Spacetime.Core.Infrastructure
{
    public interface ISpacetimeService
    {
        Task<SpacetimeResponse> Execute(SpacetimeRequest request, ResponseOptions options = null);
    }
}
