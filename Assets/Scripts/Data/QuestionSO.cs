using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Data
{
    [CreateAssetMenu(fileName = "QuestionData", menuName = "Question/QuestionData")]
    public class QuestionSO : ScriptableObject
    {
        [SerializeField]
        private string _question, _correctAnswer;
        [SerializeField]
        private string[] _answers;

        public void AssignAnswers(string question, string[] answers, string correctAnswer)
        {
            _question = question;
            _correctAnswer = correctAnswer;

            _answers = new string[answers.Length];

            int counter = 0;
            foreach (var answer in answers)
            {
                _answers[counter++] = answer;
            }
        }
    }
}