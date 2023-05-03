using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class QuestionDataManagement : JsonReader
    {
        protected jsonDataQuestions questions;

        protected override void Start()
        {
            base.Start();
            questions = new jsonDataQuestions();
            loadWebRequest = LoadData<jsonDataQuestions>();
            StartCoroutine(loadWebRequest);
        }
    }
}
