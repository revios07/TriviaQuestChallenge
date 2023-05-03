using System;
using System.Collections.Generic;

namespace Trivia.Data
{
    [Serializable]
    public class jsonDataQuestions
    {
        public List<Question> questions;
    }

    [Serializable]
    public class Question
    {
        public string category;
        public string question;
        public string[] choices;
        public string answer;
    }
}
