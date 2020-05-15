using Interfaces;
using Microsoft.Extensions.Configuration;
using Models.DTO.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationDataService : IAuthenticationDataService
    {
        public string UrlApi = "";
        public IConfiguration Configuration { get; }
        private HttpClient _httpClient;
        public AuthenticationDataService(HttpClient httpClient, IConfiguration configuration)
        {
            Configuration = configuration;
            this.UrlApi = Configuration["APPPaths:BestPathAPI"] + "Accounts/";

            this._httpClient = httpClient;
        }

        public async Task<RegisterResultDTO> Register(RegisterRequestDTO item)
        {
            var result = new RegisterResultDTO { Successful = false };
            _httpClient.BaseAddress = new Uri(this.UrlApi + "Register");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(this.UrlApi + "Register", content);

            string str = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<RegisterResultDTO>(str);
            return result;
        }

        public async Task<LoginResultDTO> Login(LoginRequestDTO item)
        {
            var result = new LoginResultDTO { Successful = false };
            _httpClient.BaseAddress = new Uri(this.UrlApi + "Login");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);

            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(this.UrlApi + "Login", content);

            string str = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<LoginResultDTO>(str);
            return result;
        }
    }
}
