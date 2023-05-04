using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class QuestionDataManagement : JsonReader
    {
        protected jsonDataQuestions[] questions;

        protected override void Start()
        {
            base.Start();

            questions = new jsonDataQuestions[1];

            loadWebRequest = new IEnumerator[questions.Length];
            jsonTexts = new string[loadWebRequest.Length];
            isLoaded = new bool[loadWebRequest.Length];

            if (jsonFile != null)
            {
                questions[0] = GetText<jsonDataQuestions>(jsonFile.text);
                return;
            }

            for (int i = 0; i < questions.Length; ++i)
            {
                loadWebRequest[i] = LoadData<jsonDataPlayers>();
                StartCoroutine(loadWebRequest[i]);
            }
        }
    }
}
