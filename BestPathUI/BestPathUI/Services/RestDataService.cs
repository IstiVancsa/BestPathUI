using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Interfaces;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;

namespace Services
{
    public class RestDataService<TModel, DTOModel> : IRestDataService<TModel, DTOModel>
                where TModel : class, IBaseModel
                where DTOModel : class, IBaseDTO
    {
        public Func<DTOModel, TModel> GetByFilterSelector = null;
        private HttpClient _httpClient;
        private IJSRuntime _jSRuntime;
        public IConfiguration Configuration { get; }
        public string UrlApi = "";
        public string Token { get; set; }
        public RestDataService(HttpClient httpClient, IConfiguration configuration, IJSRuntime JSRuntime, string partialUrl = null)
        {
            if (string.IsNullOrEmpty(partialUrl))
                partialUrl = typeof(TModel).Name.Replace("Model", "");

            Configuration = configuration;
            this.UrlApi = Configuration["APPPaths:BestPathAPI"] + partialUrl + "/";
            this._httpClient = httpClient;
            _jSRuntime = JSRuntime;
            GetToken();
        }
        private async Task GetToken()
        {
            //TODO
            Token = await _jSRuntime.InvokeAsync<string>("stateManager.load", "Token");
        }
        public async Task<bool> AddItemAsync(TModel item)
        {
            bool result = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

                var content = new StringContent(JsonConvert.SerializeObject(item.GetDTO()), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.UrlApi, content);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.UrlApi + id);
            request.PreAuthenticate = true;
            request.Headers.Add("Authorization", "Bearer " + this.Token);
            request.Method = "DELETE";
            request.ContentType = "application/json";
            var response = (await request.GetResponseAsync()) as HttpWebResponse;
            response.Close();
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<TModel> GetItemAsync(int id)
        {
            return (await this.ReturnGetHttp<List<DTOModel>>(this.UrlApi + "GetByFilter/?Id=" + id)).Select(GetByFilterSelector).FirstOrDefault();
        }

        public async Task<IList<TModel>> GetItemsAsync()
        {
            return (await this.ReturnGetHttp<List<DTOModel>>(this.UrlApi + "GetByFilter/")).Select(GetByFilterSelector).ToList();
        }

        public async Task<IList<TModel>> GetItemsAsync(string filters)
        {
            return (await this.ReturnGetHttp<List<DTOModel>>(this.UrlApi + "GetByFilter/" + filters)).Select(GetByFilterSelector).ToList();
        }

        public async Task<IList<BaseModel>> GetBaseNameModel()
        {
            return await this.ReturnGetHttp<List<BaseModel>>(this.UrlApi + "GetBaseModel/");
        }



        public async Task<bool> UpdateItemAsync(TModel item)
        {
            var uri = new Uri(string.Format(this.UrlApi));

            var serializedItem = JsonConvert.SerializeObject(item.GetDTO());
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
            var response = await this._httpClient.PutAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<TResult> ReturnGetHttp<TResult>(string url)
        {
            TResult items = default(TResult);

            var uri = new Uri(string.Format(url));
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
                var response = await this._httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    items = JsonConvert.DeserializeObject<TResult>(content);
                }
            }
            catch (Exception ex)
            {
                var dsad = ex.Message;
            }

            return items;
        }
    }
}
