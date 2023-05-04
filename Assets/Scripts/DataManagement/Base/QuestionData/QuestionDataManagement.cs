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

            for (int i = 0; i < questions.Length; ++i)
            {
                questions[i] = new jsonDataQuestions();
            }

            loadWebRequest = new IEnumerator[questions.Length];
            jsonURLs = new string[loadWebRequest.Length];
            jsonTexts = new string[loadWebRequest.Length];
            isLoaded = new bool[loadWebRequest.Length];

            for (int i = 0; i < questions.Length; ++i)
            {
                loadWebRequest[i] = LoadData<jsonDataPlayers>();
                StartCoroutine(loadWebRequest[i]);
            }
        }
    }
}
