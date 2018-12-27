using bbc.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace bbc.Functions
{
    public class CheckAnswer
    {
        // hàm kiểm tra xem câu hỏi đã được trả lời hay chưa
        public static string CheckExsist(string IDAnsQuestionVao, Dictionary<string, Answer> dicCheck, List<Answer> lstAnswer)
        {
            string ahihi = "";
            List<string> lstExistsAnswer = new List<string>();
            if (dicCheck.Count != 0)
            {
                foreach (var answer in lstAnswer)
                {
                    if (answer.AnswerID == IDAnsQuestionVao)
                    {
                        ahihi = answer.QuestionID; // lấy id quesion của câu trắc nghiệm vừa chọn
                        break;
                    }
                }

                foreach (var par in dicCheck)
                {
                    if (par.Value.QuestionID == ahihi)
                        return par.Key;
                }

            }

            return null; // chưa chọn
        }

        public static List<string> CheckExistsMultiAnswer(string IDAnsQuestionVao, Dictionary<string, Answer> dicCheck, List<Answer> lstAnswer)
        {
            string ahihi = "";
            List<string> lstExistsAnswer = new List<string>();
            if (dicCheck.Count != 0)
            {
                foreach (var answer in lstAnswer)
                {
                    if (answer.AnswerID == IDAnsQuestionVao)
                    {
                        ahihi = answer.QuestionID; // lấy id quesion của câu trắc nghiệm vừa chọn
                        break;
                    }
                }

                foreach (var par in dicCheck)
                {
                    if (par.Value.QuestionID == ahihi)
                        lstExistsAnswer.Add(par.Key);
                }
                return lstExistsAnswer;

            }

            return null; // chưa chọn
        }

        public static int CheckTracNghiem(Dictionary<string, Answer> dicCheck, List<Answer> lstAnswer)
        {
            // điểm
            int score = 0;
            List<string> lstQuestionID = dicCheck.Select(c => c.Value.QuestionID).Distinct().ToList(); // Danh sách các IdQuesion có trong danh sách câu trả lời của người dùng

            foreach (var idQuestion in lstQuestionID)
            {
                int count = 0;
                // Danh sách các câu trả lời đúng trong câu hỏi
                List<Answer> lstAnswerByQuestion = lstAnswer.Where(c => c.QuestionID == idQuestion && c.Correct == "true ").ToList();
                // Danh sách các câu trả lời của dùng trong câu hỏi
                List<Answer> lstMyAnswerInQuestion = dicCheck.Values.Where(c => c.QuestionID == idQuestion).ToList();
                if (lstMyAnswerInQuestion.Count == lstAnswerByQuestion.Count)
                {
                    foreach (var myAnswer in lstMyAnswerInQuestion)
                    {
                        foreach (var answerQuestion in lstAnswerByQuestion)
                        {
                            if (myAnswer.AnswerID == answerQuestion.AnswerID)
                            {
                                count += 1;
                                break;
                            }
                        }
                        if (count == lstAnswerByQuestion.Count)
                            score += 1;
                    }
                }
            }

            return score;
        }

        public static int CheckTuLuan(Dictionary<string, Entry> dicTuLuan, List<Answer> lstAnswer, List<Question> lstQuestion)
        {
            // điểm
            int score = 0;
            // Danh sách các câu hỏi tự luận
            List<Question> lstQuestionTuLuan = lstQuestion.Where(c => c.TypeQuestion == 2).ToList();
            foreach (var question in lstQuestionTuLuan)
            {
                int count = 0;
                List<Answer> lstAnswerTuLuan = lstAnswer.Where(c => c.QuestionID == question.QuestionID).Distinct().ToList();
                foreach (var answer in lstAnswerTuLuan)
                {
                    foreach (var myAnswer in dicTuLuan)
                    {
                        if (myAnswer.Key == answer.AnswerID
                            && myAnswer.Value.Text != null
                            && myAnswer.Value.Text.ToLower().Trim().Equals(answer.Content.ToLower().Trim()))
                        {
                            count += 1;
                            break;
                        }
                    }
                }
                if (count == lstAnswerTuLuan.Count)
                {
                    score += 1;
                }
                    
            }
            // kiểm tra câu tự luận
            //foreach (var tuLuan in dicTuLuan)
            //{
            //    int count = 0;
            //    for (int i = 0; i < lstAnswer.Count; i++)
            //    {
            //        if (tuLuan.Key.ToString() == lstAnswer[i].AnswerID && tuLuan.Value.Text != null)
            //        {
            //            if (tuLuan.Value.Text.ToLower().Trim() == lstAnswer[i].Content.ToLower().Trim())
            //            {
            //                score += 1;
            //            }
            //            break;
            //        }
            //    }
            //}

            return score;
        }
    }
}
