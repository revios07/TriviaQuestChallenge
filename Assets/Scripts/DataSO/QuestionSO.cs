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
        private List<string> _answers;

        public void AssignAnswers(string question, string[] answers, string correctAnswer)
        {
            _question = question;
            _correctAnswer = correctAnswer;

            _answers = new List<string>();

            foreach (var answer in answers)
            {
                _answers.Add(answer);
            }
        }
    }
}