using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class AssignQuestionData : QuestionDataManagement
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(NameWrite());
        }

        public JsonDataQuestions[] GetQuestions()
        {
            return questions;
        }

        private IEnumerator NameWrite()
        {
            for (int i = 0; i < questions.Length; ++i)
            {
                yield return new WaitUntil(() => IsLoaded[i]);

                if(jsonFile != null)
                {
                    questions[i] = GetText<JsonDataQuestions>(jsonFile.text);

                    Debug.Log(questions[i]);

                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                questions[i] = GetText<JsonDataQuestions>(jsonTexts[i]);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
