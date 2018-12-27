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
    public class RestAnswerService : IRestService<Answer>
    {
        HttpClient httpClient;
        public RestAnswerService()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Answer>> GetDataWithIDAsync(string id)
        {
            try
            {
                string json = await httpClient.GetStringAsync(Constant.RestAnswerURL);
                var listAnswer = JsonConvert.DeserializeObject<List<Answer>>(json);
                listAnswer = listAnswer.Where(c => c.QuestionID.Equals(id)).ToList();
                return listAnswer;
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

        public Task<bool> PostDataAsync(Answer obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutDataAsync(Answer obj, string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Answer>> GetDataAsync()
        {
            throw new NotImplementedException();
        }

    }
}
