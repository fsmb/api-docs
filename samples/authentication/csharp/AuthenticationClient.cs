/*
 * Copyright © 2018 Federation of State Medical Boards
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
 * documentation files (the “Software”), to deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit
 * persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
 * COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// NOTE: This code uses JSON.NET to serialize JSON. Include the NewtonSoft.Json NuGet package to compile.
using Newtonsoft.Json;

namespace Fsmb.Apis.Authentication
{
    /// <summary>Provides a client for authenticating to FSMB APIs.</summary>
    /// <remarks>
    /// Create an instance of the client to authenticate to the API. The constructor requires an <see cref="
    /// HttpClient"/> instance configured to use the appropriate authentication URL. It is strongly recommended that applications
    /// create only one instance of the <see cref="HttpClient"/> per URL and reuse it throughout the application's lifetime. Refer
    /// to https://blogs.msdn.microsoft.com/shacorn/2016/10/21/best-practices-for-using-httpclient-on-services/ for more information.
    /// </remarks>
    public class AuthenticationClient
    {
        /// <summary>Initializes an instance of the <see cref="AuthenticationClient"/> class.</summary>
        /// <param name="client">The client associated with the authentication URL.</param>
        public AuthenticationClient ( HttpClient client )
        {
            _client = client;
        }

        /// <summary>Authenticates a client.</summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scopes">The list of scopes.</param>
        /// <returns>The access token.</returns>
        public string Authenticate ( string clientId, string clientSecret, params string[] scopes )
        {
            try
            {
                return AuthenticateAsync(clientId, clientSecret, scopes).Result;
            } catch (AggregateException e)
            {
                throw e.InnerException;
            };
        }

        /// <summary>Authenticates a client.</summary>
        /// <param name="clientId">The client ID.</param>
        /// <param name="clientSecret">The client secret.</param>
        /// <param name="scopes">The list of scopes.</param>
        /// <returns>The access token.</returns>
        public async Task<string> AuthenticateAsync ( string clientId, string clientSecret, params string[] scopes )
        {
            if (String.IsNullOrEmpty(clientId))
                throw new ArgumentException("Client ID is required.", nameof(clientId));
            if (String.IsNullOrEmpty(clientSecret))
                throw new ArgumentException("Client secret is required.", nameof(clientSecret));

            var message = new HttpRequestMessage(HttpMethod.Post, "connect/token");
            message.Headers.Add("Accept", "application/json");

            var request = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("scope", String.Join(" ", scopes)),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };
            message.Content = new FormUrlEncodedContent(request);

            using (var response = await _client.SendAsync(message, CancellationToken.None).ConfigureAwait(false))
            {
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var token = JsonConvert.DeserializeObject<BearerToken>(body);

                return token.access_token;
            };
        }

        private sealed class BearerToken
        {
            public string access_token { get; set; }
        }

        private readonly HttpClient _client;
    }
}
