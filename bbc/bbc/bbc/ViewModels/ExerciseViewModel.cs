using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bbc.Data.Models;
using bbc.Data.Services.Online;
using bbc.Functions;
using bbc.Interfaces;
using Xamarin.Forms;

namespace bbc.ViewModels
{
    class ExerciseViewModel : BaseViewModel
    {
        #region Attributes
        private  RestQuestionService restQuestionService = null;
        private RestAnswerService resAnswerService = null;

        //private List<Question> _listQuestion { get; set; } // Danh sách các question của 1 Lesson
        public List<Question> lstQuestion { get; set; } = new List<Question>(); // Danh sách câu hỏi của 1 Lesson
        public List<Answer> lstAnswerLesson { get; set; } = new List<Answer>(); // Danh sách tất cả câu trả lời có trong Lesson.
        public List<Answer> lstAnswer { get; set; } = new List<Answer>(); // Danh sách câu trả lời trong 1 question
        public StackLayout myLayout = new StackLayout { Padding = new Thickness(5, 10) };
        public Dictionary<string, Button> dicButton = new Dictionary<string, Button>(); // Danh sách các button trắc nghiệm được tạo
        public Dictionary<string, Answer> dicAnswerUser = new Dictionary<string, Answer>(); // Danh sách lưu các câu trả lời của người dùng
        public Dictionary<string, Entry> dicTuLuan = new Dictionary<string, Entry>(); // Danh sách các câu trả lời tự luận của người dùng
        // private List<Answer> _listAnswer { get; set; } // Danh sách các câu trả lời

        //public QuestionDatabaseAccess questionDb;
        //public AnswerDatabaseAccess answerDb;

        #endregion

        #region Properties
        //public List<Question> ListQuestion
        //{
        //    get
        //    {
        //        return _listQuestion;
        //    }
        //    set
        //    {
        //        _listQuestion = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public List<Answer> ListAnswer
        //{
        //    get
        //    {
        //        return _listAnswer;
        //    }
        //    set
        //    {
        //        _listAnswer = value;
        //        OnPropertyChanged();
        //    }
        //}
        #endregion

        #region Methods
        private async Task GetDataQuestion(string idLesson)
        {
            //ListQuestion = new List<Question>();
            lstQuestion = new List<Question>();
            restQuestionService = new RestQuestionService();
            Task<List<Question>> ahihi = restQuestionService.GetDataWithIDAsync(idLesson);
            //Task.WhenAll(ahihi);
            //ListQuestion = await ahihi;
            lstQuestion = await ahihi;
            // lstQuestion = ahihi.Result;
        } // Không dùng

        private async void GetDataAnswer(string idQuestion)
        {
            //lstAnswer = new List<Answer>();
            resAnswerService = new RestAnswerService();
            Task<List<Answer>> ahihi = resAnswerService.GetDataWithIDAsync(idQuestion);
            lstAnswer = await ahihi;
        } // Không dùng

        public async void GetData(StackLayout myLayout, string idLesson)
        {
            // Offline = true;
            // Lấy danh sách các câu hỏi.
            //if (myOffline) // Đang Offline
            //{
            //    //questionDb = new QuestionDatabaseAccess();
            //    //lstQuestion = questionDb.GetQuestionDb(idLesson);
            //    lstQuestion = GetDataOffline.getQuetsionOffline(idLesson);
            //}
            //else // Đang Online
            //{
                restQuestionService = new RestQuestionService();
                Task<List<Question>> questionAsync = restQuestionService.GetDataWithIDAsync(idLesson);
                lstQuestion = await questionAsync;
            //

            foreach (var question in lstQuestion)
            {
                var myLableQuestion = new Label
                {
                    Text = "" + question.Content,
                    FontAttributes = FontAttributes.Bold
                };
                myLayout.Children.Add(myLableQuestion);

                // Lay cac cau tra loi trong cau hoi
                //if (myOffline)
                //{
                //    //answerDb = new AnswerDatabaseAccess();
                //    //lstAnswer = answerDb.GetAnswerDb(question.QuestionID);

                //    lstAnswer = GetDataOffline.getAnswerOffline(question.QuestionID);
                //}
                //else
                //{
                    resAnswerService = new RestAnswerService();
                    Task<List<Answer>> answerAsync = resAnswerService.GetDataWithIDAsync(question.QuestionID);
                    lstAnswer = new List<Answer>();
                    lstAnswer = await answerAsync;
                //}

                if (question.TypeQuestion == 1 || question.TypeQuestion == 3) // Trac Nghiem
                {
                    foreach (var answer in lstAnswer)
                    {
                        lstAnswerLesson.Add(answer);
                        Xamarin.Forms.Button myAnswer = new Xamarin.Forms.Button
                        {
                            Text = "" + answer.Content,
                            Margin = 10
                            ,
                            // Command = CheckAnswerCommand(answer.AnswerID)
                            Command = CheckAnswerCommand(answer)

                        };

                        dicButton.Add(answer.AnswerID, myAnswer); // Danh sách lưu các button trắc nghiệm
                        myLayout.Children.Add(myAnswer);
                    }
                }
                else // Tu luan
                {
                    foreach (var answer in lstAnswer)
                    {
                        lstAnswerLesson.Add(answer);
                        var myEntryAnswer = new Entry
                        {
                            Placeholder = "" + answer.Content
                        };
                        //dicTuLuan.Add(ans[k].IDAnswer, myEntryAnswer);
                        dicTuLuan.Add(answer.AnswerID, myEntryAnswer);
                        myLayout.Children.Add(myEntryAnswer);
                    }
                }
            }

            Xamarin.Forms.Button myFinishButton = new Xamarin.Forms.Button
            {
                Text = "Finish"
                ,
                Command = FinishCommand()
            };

            Xamarin.Forms.Button myRefeshButton = new Xamarin.Forms.Button
            {
                Text = "Refesh"
                ,
                Command = FinishCommand()
            };
            myLayout.Children.Add(myFinishButton);
        }

        public StackLayout createAuto(Lesson lesson)
        {
            // Gán Tittle
            Title = "Exam of " + lesson.Name;

            GetData(myLayout, lesson.Id);

            return myLayout;
        }
        #endregion

        #region Commands
        public Command CheckAnswerCommand(Answer answer) // check: IdAnswer
        {
            return new Command(() =>
            {
                foreach (var question in lstQuestion)
                {
                    if (answer.QuestionID == question.QuestionID)
                    {
                        if (question.TypeQuestion != 3) // Không phải MultiAnswer
                        {
                            string exsist = CheckAnswer.CheckExsist(answer.AnswerID, dicAnswerUser, lstAnswerLesson);
                            // Nếu đã button trong câu hỏi đã được nhấn
                            if (exsist != null)
                            {
                                if (exsist == answer.AnswerID)
                                {
                                    dicAnswerUser.Remove(exsist); // Xóa button cũ khỏi danh sách câu trả lời của người dùng
                                    this.dicButton[exsist].BackgroundColor = Color.Default; // Chuyển button về màu ban đầu
                                    break;
                                }
                                dicAnswerUser.Remove(exsist); // Xóa button cũ khỏi danh sách câu trả lời của người dùng
                                this.dicButton[exsist].BackgroundColor = Color.Default; // Chuyển button về màu ban đầu
                            }
                            dicAnswerUser.Add(answer.AnswerID, answer); // Thêm button vừa nhấn vào danh sách câu trả lời của người dùng
                            this.dicButton[answer.AnswerID].BackgroundColor = Color.Red;
                        }
                        else
                        {
                            bool isChanged = false;
                            List<string> lstExistsAnswer = CheckAnswer.CheckExistsMultiAnswer(answer.AnswerID, dicAnswerUser, lstAnswerLesson);
                            // string exsist = CheckAnswer.CheckExsist(answer.AnswerID, dicAnswerUser, lstAnswerLesson);
                            if (lstExistsAnswer != null)
                            {
                                foreach (var exsist in lstExistsAnswer)
                                {
                                    if (exsist == answer.AnswerID)
                                    {
                                        dicAnswerUser.Remove(exsist); // Xóa button cũ khỏi danh sách câu trả lời của người dùng
                                        this.dicButton[exsist].BackgroundColor = Color.Default; // Chuyển button về màu ban đầu
                                        isChanged = true;
                                        break;
                                    }
                                }
                            }
                            if(!isChanged)
                            {
                                dicAnswerUser.Add(answer.AnswerID, answer); // Thêm button vừa nhấn vào danh sách câu trả lời của người dùng
                                this.dicButton[answer.AnswerID].BackgroundColor = Color.Red;
                            }
                        }
                    }
                }
            }
            );
        }

        private Command FinishCommand()
        {
            return new Command(() =>
            {
                // Điểm
                int score = CheckAnswer.CheckTracNghiem(dicAnswerUser, lstAnswerLesson) + CheckAnswer.CheckTuLuan(dicTuLuan, lstAnswerLesson, lstQuestion);

                foreach (var question in lstQuestion)
                {
                    List<Answer> lstAnswerDung = lstAnswerLesson.Where
                                                                (c => c.QuestionID == question.QuestionID && c.Correct == "true ")
                                                                .ToList(); // Danh sách các câu trả lời đúng có trong câu hỏi

                    if (question.TypeQuestion == 1 || question.TypeQuestion == 3) // Trắc Nghiệm
                    {
                        foreach (var answer in lstAnswerDung)
                        {
                            dicButton[answer.AnswerID].BackgroundColor = Color.Green;
                        }
                    }
                    else // Tự Luận
                    {
                        foreach (var answer in lstAnswerDung)
                        {
                            if (dicTuLuan[answer.AnswerID].Text != null
                                && dicTuLuan[answer.AnswerID].Text.ToLower().Equals(answer.Content.ToLower().Trim()))
                            {
                                dicTuLuan[answer.AnswerID].BackgroundColor = Color.Green;
                            }
                            else
                                dicTuLuan[answer.AnswerID].BackgroundColor = Color.Red;
                        }
                    }
                }
                DependencyService.Get<IMessage>().ShortToast("Your Score: " + score);
            }
            );
        }
        #endregion
    }
}
