using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.WebUtilities.QueryHelpers;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;
//using AutoMapper.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace FoodApp
{
    public enum Service { ServerA, ServerB }
    public class HttpUtils
    {
        private readonly IConfiguration _config;
        private readonly ILogger<HttpUtils> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HttpUtils(
            IConfiguration config,
            ILogger<HttpUtils> logger,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _config = config;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }

        public string GetBaseUri(Service service, string endPoint)
        {
            return GetBaseUri(service, endPoint, default);
        }

        public string GetBaseUri(
            Service service,
            string endPoint,
            Dictionary<string,string>? uriParams)
        {
 
            var serviceName = service.ToString();
            string hostUri = _config.GetSection(serviceName).GetSection("ServiceUri").Value;
            string apiVersion = _config.GetSection(serviceName).GetSection("ApiVersion").Value;
            var actualEndPointNameSection = _config.GetSection(serviceName).GetSection(endPoint);
            var testOverrideUri = actualEndPointNameSection?.GetSection("TestOverrideUri").Value;

            if ((testOverrideUri?.Trim() ?? "") != "")
            {
                return testOverrideUri;
            }
            var actualEndPointName = actualEndPointNameSection?.GetSection("Uri").Value;
            actualEndPointName = actualEndPointName ?? actualEndPointNameSection.Value;

            if ((hostUri == default) || (apiVersion == default) || (actualEndPointName == default))
            {
                throw new Exception($"Unable to map end point {endPoint}");
            }

            string uri = hostUri + "/";
            if ((apiVersion != null) && (apiVersion != ""))
            {
                uri += apiVersion + "/";
            }
            uri += actualEndPointName;

            if (uriParams != default)
            {
                uri = AddQueryString(uri, uriParams);
            }

            return uri;
        }

        public async Task<Stream> HttpSendAsync(HttpClient httpClient, string fullUri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, fullUri);
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed: http get request. Status code: {response.StatusCode} ,for uri: {fullUri}");
            }
            var responseStream = await response.Content.ReadAsStreamAsync();

            if (true /*debug only*/)
            {
                var str = await response.Content.ReadAsStringAsync();
                _logger.LogDebug($"HttpSendAsync(): Uri: {fullUri} \n Retrieved data:{str}");
            }

            return responseStream;

        }
    }
}
