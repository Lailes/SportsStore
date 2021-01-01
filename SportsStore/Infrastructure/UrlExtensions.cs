﻿using System;
using Microsoft.AspNetCore.Http;

namespace SportsStore.Infrastructure {
    public static class UrlExtensions {

        public static string PathAndQuery(this HttpRequest request) {
            var path = request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
            if (request.QueryString.HasValue) {
                Console.WriteLine(request.Path + "<=>" + request.QueryString);
            }
            return path;
        }
    }
}