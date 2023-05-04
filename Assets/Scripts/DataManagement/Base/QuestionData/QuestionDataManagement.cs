using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class QuestionDataManagement : JsonReader
    {
        protected JsonDataQuestions[] questions;

        protected override void Start()
        {
            base.Start();

            questions = new JsonDataQuestions[1];

            loadWebRequest = new IEnumerator[questions.Length];
            jsonTexts = new string[loadWebRequest.Length];
            IsLoaded = new bool[loadWebRequest.Length];

            if (jsonFile != null)
            {
                questions[0] = GetText<JsonDataQuestions>(jsonFile.text);
                return;
            }

            for (int i = 0; i < questions.Length; ++i)
            {
                loadWebRequest[i] = LoadData<JsonDataPlayers>();
                StartCoroutine(loadWebRequest[i]);
            }
        }
    }
}
