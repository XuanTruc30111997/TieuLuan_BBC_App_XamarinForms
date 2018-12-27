using System;
using System.Collections.Generic;
using System.Text;

namespace AppConfig
{
    public class Constant
    {
        #region url api
        public const string RestTopicURL = "https://bbclearningenglish.azurewebsites.net/api/Topics";
        public const string RestLessonURL= "https://bbclearningenglish.azurewebsites.net/api/Lessons";
        public const string RestQuestionURL = "https://bbclearningenglish.azurewebsites.net/api/Questions";
        public const string RestAnswerURL = "https://bbclearningenglish.azurewebsites.net/api/Answers";
        #endregion

        public const string play_image = "play.png";
        public const string pause_image = "pause.png";
        public const string localOfflineDB = "BBCLocalDB";
    }
}
