﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace UsedCables.Infrastructure.Helpers.RestHelper
{
    public sealed class RestClientHelper : IRestClientHelper
    {
        public async Task<string> GetAsync(string requestUri, Dictionary<string, string>? additionalHeaders = null)
        {
            var timer = new Stopwatch();
            timer.Start();
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new())
            {
                //Uncomment below line to Skip cert validation check
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }

                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    result = await httpClient.GetStringAsync(requestUri);
                    timer.Stop();
                }
            }
            return result;
        }

        public async Task<string> PostAsync<T>(string requestUri, T request, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            var timer = new Stopwatch();
            timer.Start();
            string result = "Error";
            try
            {
                using (HttpClientHandler httpClientHandler = new HttpClientHandler())
                {
                    //Uncomment below line to Skip cert validation check
                    //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }

                    using (HttpClient httpClient = new(httpClientHandler))
                    {
                        AddHeaders(httpClient, additionalHeaders);
                        result = await httpClient.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(request))
                        {
                            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                        }).Result.Content.ReadAsStringAsync();
                        timer.Stop();
                    }
                }
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public async Task<string> DeleteAsync(string requestUri, Dictionary<string, string>? additionalHeaders = null)
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new())
            {
                //Uncomment below line to Skip cert validation check
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }

                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);
                    var httpResponseMessage = await httpClient.DeleteAsync(requestUri);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> PutAsync<T>(string requestUri, T request, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new())
            {
                //Uncomment below line to Skip cert validation check
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }

                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);

                    var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    var httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponseMessage = await httpClient.PutAsync(requestUri, httpContent);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        public async Task<string> PatchAsync<T>(string requestUri, T request, Dictionary<string, string>? additionalHeaders = null) where T : class
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new())
            {
                //Uncomment below line to Skip cert validation check
                //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }

                using (HttpClient httpClient = new(httpClientHandler))
                {
                    AddHeaders(httpClient, additionalHeaders);

                    var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    var httpContent = new StringContent(json);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var httpResponseMessage = await httpClient.PatchAsync(requestUri, httpContent);
                    result = await httpResponseMessage.Content.ReadAsStringAsync();
                }
            }
            return result;
        }

        private static void AddHeaders(HttpClient httpClient, Dictionary<string, string>? additionalHeaders)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            //No additional headers to be added
            if (additionalHeaders == null)
                return;

            foreach (KeyValuePair<string, string> current in additionalHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(current.Key, current.Value);
            }
        }
    }
}