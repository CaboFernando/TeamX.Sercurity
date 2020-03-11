﻿using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;

namespace TeamX.Security.Client
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            // discover endpoints from the metadata by calling Auth server hosted on 5000 port
            var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoveryClient.IsError)
            {
                Console.WriteLine(discoveryClient.Error);
                return;
            }

            // request the token from the Auth server
            var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "client", "secret");
            var response = await tokenClient.RequestClientCredentialsAsync("apil");

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            var apiResponse = await client.GetAsync("http://localhost:5001/identity");

            if(!apiResponse.IsSuccessStatusCode)
                Console.WriteLine(apiResponse.StatusCode);
            else
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

    }

}