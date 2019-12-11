using Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.Interfaces;

namespace EmployeesMobile
{
    public class RestDataService<T> : IRestDataService<T>
                where T : class, IBaseModel
    {
        private HttpClient client;

        public string UrlApi = "";
        //public string Token => Token must be store in the app somewhere
        public RestDataService(string partialUrl = null)
        {
            if (string.IsNullOrEmpty(partialUrl))
                partialUrl = typeof(T).Name.Replace("Model", "");

            this.UrlApi = Utils.Constants.BaseUrlApi + partialUrl + "/";
            this.client = new HttpClient();
        }
        public async Task<bool> AddItemAsync(T item)
        {
            bool result = true;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);

                var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(this.UrlApi, content);

                result = response.IsSuccessStatusCode;
            }

            return result;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            bool result = true;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.UrlApi + id);
            request.Method = "DELETE";
            request.ContentType = "application/json";

            var response = (await request.GetResponseAsync()) as HttpWebResponse;
            result = response.StatusCode == HttpStatusCode.OK;

            return result;
        }

        public async Task<T> GetItemAsync(int id)
        {
            return (await this.ReturnGetHttp<List<T>>(this.UrlApi + "GetByFilter/?Id=" + id)).FirstOrDefault();
        }

        public async Task<IList<T>> GetItemsAsync()
        {
            return await this.ReturnGetHttp<List<T>>(this.UrlApi + "GetByFilter/");
        }

        public async Task<IList<T>> GetItemsAsync(string filters)
        {
            return await this.ReturnGetHttp<List<T>>(this.UrlApi + "GetByFilter/" + filters);
        }

        public async Task<IList<BaseModel>> GetBaseNameModel()
        {
            return await this.ReturnGetHttp<List<BaseModel>>(this.UrlApi + "GetBaseModel/");
        }

        

        public async Task<bool> UpdateItemAsync(T item)
        {
            bool result = true;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.UrlApi + item.Id);
            request.Method = "PUT";
            request.ContentType = "application/json";

            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");
            byte[] bytes = Encoding.ASCII.GetBytes(serializedItem);

            using (var requestStream = request.GetRequestStream())
                requestStream.Write(bytes, 0, bytes.Length);

            var response = (await request.GetResponseAsync()) as HttpWebResponse;
            result = response.StatusCode == HttpStatusCode.OK;
            return result;
        }

        public async Task<TResult> ReturnGetHttp<TResult>(string url)
        {
            TResult items = default(TResult);

            var uri = new Uri(string.Format(url));
            try
            {

                //this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this.Token);
                var response = await this.client.GetAsync(uri);
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
