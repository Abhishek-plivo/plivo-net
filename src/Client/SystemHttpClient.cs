using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plivo.Authentication;
using Plivo.Http;
using Plivo.test;

namespace Plivo.Client
{
    /// <summary>
    /// Rest sharp client.
    /// </summary>
    public class SystemHttpClient : IHttpClient
    {
        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public System.Net.Http.HttpClient _client { get; set; }

        public string RuntimeVersion;

        private JsonSerializerSettings _jsonSettings;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:plivo.Client.SystemHttpClient"/> class.
        /// </summary>
        /// <param name="basicAuth">Basic auth.</param>
        /// <param name="proxyServerSettings">Proxy settings.</param>
        public SystemHttpClient(BasicAuth basicAuth, Dictionary<string, string> proxyServerSettings)
        {
            Type type = Type.GetType("Mono.Runtime");
            if (type != null)
            {
                MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
                if (displayName != null)
                    RuntimeVersion = "Mono " + displayName.Invoke(null, null);
            }
            else
            {
                RuntimeVersion = Environment.Version.ToString();
            }
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
//                Credentials = new NetworkCredential(basicAuth.AuthId, basicAuth.AuthToken),
                PreAuthenticate = true,
                UseDefaultCredentials = false
            };
            _client = new System.Net.Http.HttpClient(httpClientHandler);
            var encoding = new ASCIIEncoding();
            var authHeader = 
                new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(
                        encoding.GetBytes(
                            $"{basicAuth.AuthId}:{basicAuth.AuthToken}"
                        )
                    )
                );
            _client.DefaultRequestHeaders.Authorization = authHeader;
            _client.DefaultRequestHeaders.Add("User-Agent", "plivo-dotnet/" +
                                                            Version.SdkVersion +
                                                            " (" +
                                                            RuntimeVersion +
                                                            ")");
            _client.BaseAddress = new Uri("https://api.plivo.com/" + Version.ApiVersion + "/");
            
            _jsonSettings = new JsonSerializerSettings  
            {  
                ContractResolver = new PascalCasePropertyNamesContractResolver(),  
                NullValueHandling = NullValueHandling.Ignore  
            };
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <returns>The request.</returns>
        /// <param name="method">Method.</param>
        /// <param name="uri">URI.</param>
        /// <param name="data">Data.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public PlivoResponse<T> SendRequest<T>(string method, string uri, Dictionary<string, object> data)
            where T : new()
        {   
            HttpResponseMessage response = null;
            HttpRequestMessage request = null;

            Console.WriteLine("uri: " + uri);
            Console.WriteLine("method: " + method);
            Console.WriteLine("data: " + JsonConvert.SerializeObject(data));
            
            switch (method)
            {
                case "GET":
                    request = new HttpRequestMessage(HttpMethod.Get, uri + AsQueryString(data));
                    request.Headers.Add("Accept", "application/json");
                    break;
                case "POST":
                    request = new HttpRequestMessage(HttpMethod.Post, uri);
                    request.Headers.Add("Accept", "application/json");
                    request.Content = new StringContent(
                        JsonConvert.SerializeObject(data),
                        Encoding.UTF8,
                        "application/json"
                    );
                    break;
                case "DELETE":
                    request = new HttpRequestMessage(HttpMethod.Delete, uri + AsQueryString(data));
                    request.Headers.Add("Accept", "application/json");
                    break;
                default:
                    throw new NotSupportedException(
                        method + " not supported");
            }

            response = _client.SendAsync(request).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            
            // create Plivo response object along with the deserialized object
            PlivoResponse<T> plivoResponse =
                new PlivoResponse<T>(
                    (uint)response.StatusCode.GetHashCode(),
                    response.Headers.Select(item => item.Key + "=" + item.Value).ToList(),
                    responseContent,
                    responseContent != string.Empty ?
                        JsonConvert.DeserializeObject<T>(responseContent, _jsonSettings):
                        new T(),
                    new PlivoRequest(method, uri, request.Headers.ToString(), data));
            
            return plivoResponse;
        }
        
        public static string AsQueryString(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (!parameters.Any())
                return "";

            var builder = new StringBuilder("?");

            var separator = "";
            foreach (var kvp in parameters.Where(kvp => kvp.Value != null))
            {
                builder.AppendFormat("{0}{1}={2}", separator, WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value.ToString()));

                separator = "&";
            }

            return builder.ToString();
        }
    }
}
