using Flurl;

namespace Spacetime.Core
{
    public class UrlBuilder
    {
        public string GetUrl(SpacetimeRequest request)
        {
            var url = AddQueryParams(request.URL, request.QueryParams);

            return url;
        }

        public static string AddQueryParams(string str, List<QueryParamDto> parameters)
        {
            foreach (var p in parameters)
            {
                str = str.SetQueryParam(p.Name, p.Value);
            }

            return str;
        }
    }
}
