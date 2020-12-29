using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure {
    public static class UrlExtensions {

        public static string PathAndQuary(this HttpRequest request) =>
            request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
    }
}