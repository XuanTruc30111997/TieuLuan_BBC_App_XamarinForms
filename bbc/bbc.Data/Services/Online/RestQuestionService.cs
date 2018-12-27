using AppConfig;
using bbc.Data.Interfaces;
using bbc.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bbc.Data.Services.Online
{
    public class RestQuestionService : IRestService<Question>
    {
        HttpClient httpClient;
        public RestQuestionService()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Question>> GetDataWithIDAsync(string id)
        {
            try
            {
                string json = await httpClient.GetStringAsync(Constant.RestQuestionURL);
                var listQuestion = JsonConvert.DeserializeObject<List<Question>>(json);
                listQuestion = listQuestion.Where(c => c.LessonID.Equals(id)).ToList();
                return listQuestion;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostDataAsync(Question obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutDataAsync(Question obj, string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Question>> GetDataAsync()
        {
            try
            {
                string json = await httpClient.GetStringAsync(Constant.RestQuestionURL);
                var listQuestion = JsonConvert.DeserializeObject<List<Question>>(json);
                return listQuestion;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
