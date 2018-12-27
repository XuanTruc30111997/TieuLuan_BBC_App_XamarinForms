using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using bbc.Data.Interfaces;
using bbc.Data.Models;
using AppConfig;
using Newtonsoft.Json;

namespace bbc.Data.Services
{
    public class RestTopicService: IRestService<Topic>
    {
        HttpClient httpClient;
        public RestTopicService()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Topic>> GetDataAsync()
        {
            try
            {
                string json = await httpClient.GetStringAsync(Constant.RestTopicURL);
                var listTopics = JsonConvert.DeserializeObject<List<Topic>>(json);
                return listTopics;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Task<bool> PostDataAsync(Topic obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutDataAsync(Topic obj, string id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
