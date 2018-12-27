using AppConfig;
using bbc.Data.Interfaces;
using bbc.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace bbc.Data.Services
{
    public class RestLessonService: IRestService<Lesson>
    {
        HttpClient httpClient;
        public RestLessonService()
        {
            httpClient = new HttpClient();
            httpClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Lesson>> GetDataAsync()
        {
            try
            {
                string json = await httpClient.GetStringAsync(Constant.RestLessonURL);
                var listLessons = JsonConvert.DeserializeObject<List<Lesson>>(json);
                return listLessons;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task<Lesson> GetLessonByID(string lessonID)
        {
            try
            {
                var listLesson = await GetDataAsync();
                foreach(var lesson in listLesson)
                {
                    if(lesson.Id.Trim().Equals(lessonID.Trim()))
                    {
                        return lesson;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        public async Task<List<Lesson>> GetDataByTopic(string topicID)
        {
            try
            {
                List<Lesson> listLessonByTopic = new List<Lesson>();
                var listLessons = await GetDataAsync();
                foreach(var lesson in listLessons)
                {
                    if(lesson.IdTP.Trim().Equals(topicID.Trim()))
                    {
                        listLessonByTopic.Add(lesson);
                    }
                }
                return listLessonByTopic;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        public Task<bool> PostDataAsync(Lesson obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PutDataAsync(Lesson obj, string id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteDataAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
