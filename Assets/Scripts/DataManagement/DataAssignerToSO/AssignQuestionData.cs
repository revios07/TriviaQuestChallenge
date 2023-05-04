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

        public jsonDataQuestions[] GetQuestions()
        {
            return questions;
        }

        private IEnumerator NameWrite()
        {
            for (int i = 0; i < questions.Length; ++i)
            {
                yield return new WaitUntil(() => isLoaded[i]);

                if(jsonFile != null)
                {
                    questions[i] = GetText<jsonDataQuestions>(jsonFile.text);

                    Debug.Log(questions[i]);

                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                questions[i] = GetText<jsonDataQuestions>(jsonTexts[i]);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
