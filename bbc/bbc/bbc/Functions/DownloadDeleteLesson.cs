using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using bbc.Data.Models;
using bbc.Data.Services.Offline;
using bbc.Data.Services.Online;
using bbc.Interfaces;
using Plugin.DownloadManager;
using Xamarin.Forms;

namespace bbc.Functions
{
    public class DownloadDeleteLesson
    {
        QuestionOfflineService questionDb;
        AnswerOfflineService answerDb;
        LessonOfflineService lessonDb;

        public async Task DownloadLesson(List<Lesson> ListLesson, string idLesson)
        {
            string nameLesson = null; //lưu tên Lesson để thông báo
                                      // Download Lesson
            foreach (var myLesson in ListLesson)
            {
                if (myLesson.Id.Equals(idLesson))
                {
                    nameLesson = myLesson.Name.Trim();
                    string urlAudio = myLesson.FileURLOnline.ToString();
                    lessonDb = new LessonOfflineService();
                    // lessonDb.AddLesson(myLesson);

                    lessonDb.InsertLessonToLocalDatabase(myLesson.Id, myLesson.Name, myLesson.Year,
                        myLesson.IdTP, myLesson.Transcript, myLesson.Actor, myLesson.Sumary, myLesson.Vocabulary);

                    await Task.Run(() =>
                    {
                        var downloadManager = CrossDownloadManager.Current;
                        var file = downloadManager.CreateDownloadFile(urlAudio);
                        downloadManager.Start(file, true);
                    });

                    break;
                }
            }

            RestQuestionService resQuestionService = new RestQuestionService();
            List<Question> myLstQuestion = await resQuestionService.GetDataWithIDAsync(idLesson); // Lấy các câu hỏi có trong Lesson
            questionDb = new QuestionOfflineService();
            answerDb = new AnswerOfflineService();
            foreach (var myQuestion in myLstQuestion)
            {
                questionDb.AddQuestion(myQuestion); // Download Question
                RestAnswerService resAnswerService = new RestAnswerService();
                List<Answer> myLstAnswer = await resAnswerService.GetDataWithIDAsync(myQuestion.QuestionID); // Lấy các câu trả lời có trong cầu hỏi
                foreach (var myAnswer in myLstAnswer)
                    answerDb.AddAnswer(myAnswer); //Download Answer
            }

            DependencyService.Get<IMessage>().LongToast("Downloadi " + nameLesson + "Completed");
        }

        public async Task DeleteLesson(string idLesson)
        {
            questionDb = new QuestionOfflineService();
            List<Question> myLstQuestion = new List<Question>();
            myLstQuestion = questionDb.GetQuestionDb(idLesson);
            foreach (var myQuestion in myLstQuestion)
            {
                // Thực hiện xóa câu trả lời
                answerDb = new AnswerOfflineService();
                answerDb.DeleteAnswer(myQuestion.QuestionID);
            }
            // Thực hiện xóa câu hỏi
            questionDb.DeleteQuestion(idLesson);
            // Thực hiện xóa Lesson
            lessonDb = new LessonOfflineService();
            lessonDb.DeleteLesson(idLesson);

            DependencyService.Get<IMessage>().ShortToast("Delete completed");
        }

        public bool CheckLesson(string idLesson)
        {
            List<Lesson> myListLessonOffline = new List<Lesson>();
            myListLessonOffline = GetDataOffline.getLessonOffline();
            if (myListLessonOffline != null)
            {
                foreach (var myLessonOffline in myListLessonOffline)
                {
                    if (myLessonOffline.Id == idLesson)
                        return true;
                }
            }
            return false;
        }
    }
}
