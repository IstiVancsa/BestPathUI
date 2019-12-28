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

namespace Services
{
    public class RestDataService<TModel, DTOModel> : IRestDataService<TModel, DTOModel>
                where TModel : class, IBaseModel
                where DTOModel : class, IBaseDTO
    {
        public Func<DTOModel, TModel> GetByFilterSelector = null;
        private HttpClient _httpClient;

        public string UrlApi = "";
        //public string Token => Token must be store in the app somewhere
        public RestDataService(HttpClient httpClient, string partialUrl = null)
        {
            if (string.IsNullOrEmpty(partialUrl))
                partialUrl = typeof(TModel).Name.Replace("Model", "");

            this.UrlApi = Utils.Constants.BaseUrlApi + partialUrl + "/";
            this._httpClient = httpClient;
        }
        public async Task<bool> AddItemAsync(TModel item)
        {
            bool result = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);

                var content = new StringContent(JsonConvert.SerializeObject(item.GetDTO()), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.UrlApi, content);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.UrlApi + id);
            request.Method = "DELETE";
            request.ContentType = "application/json";

            var response = (await request.GetResponseAsync()) as HttpWebResponse;
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

            var response = await this._httpClient.PutAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<TResult> ReturnGetHttp<TResult>(string url)
        {
            TResult items = default(TResult);

            var uri = new Uri(string.Format(url));
            try
            {

                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);
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
