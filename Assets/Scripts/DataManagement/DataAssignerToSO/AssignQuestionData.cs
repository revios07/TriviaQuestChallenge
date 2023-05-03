using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.DataManagement;

namespace Trivia.Data
{
    public class AssignQuestionData : QuestionDataManagement
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(NameWrite());
        }

        private IEnumerator NameWrite()
        {
            yield return new WaitUntil(() => isLoaded);

            questions = GetText<jsonDataQuestions>(jsonText);
            Debug.Log(questions.questions[0].question);
        }

        public jsonDataQuestions GetQuestions()
        {
            return questions;
        }
    }
}
