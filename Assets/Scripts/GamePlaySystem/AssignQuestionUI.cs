using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trivia.Data;
using Trivia.DataManagement;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AssignQuestionUI : MonoBehaviour
    {
        private int _questionIndex = 0;

        private AssignQuestionData _assignQuestionData;
        private TMP_Text _questionText;

        private Button[] _selectableButtons;
        private Button _correctAnswerButton;
        private List<TMP_Text> _choicesTextes = new List<TMP_Text>();

        private void Awake()
        {
            _assignQuestionData = FindObjectOfType<AssignQuestionData>();
            _selectableButtons = GetComponentsInChildren<Button>();

            var counter = 0;
            foreach (var transform in _selectableButtons)
            {
                _choicesTextes.Add(transform.GetComponentInChildren<TMP_Text>());
                _selectableButtons[counter++].onClick.RemoveAllListeners();
            }
        }

        private void AssignQuestion()
        {
            if (_questionIndex >= _assignQuestionData.GetQuestions()[0].questions.Count)
            {
                Debug.Log("Question Reached Out!");
                Debug.LogError("Turn To First Question!");
                _questionIndex = 0;
            }

            var question = _assignQuestionData.GetQuestions()[0].questions[_questionIndex];
            _questionText.text = question.question;

            for (var i = 0; i < question.choices.Length; ++i)
            {
                _choicesTextes[i].text = question.choices[i];

                //Check Which Choise is Correct On Button
                if (_choicesTextes[i].text.StartsWith(question.answer))
                {
                    //Correct Answer
                    _correctAnswerButton = _selectableButtons[i];
                    _correctAnswerButton.GetComponent<AnswerAnimation>().IsCorrectAnswerButton(true);
                    _correctAnswerButton.onClick.AddListener(() => EventsSystem.correctAnswer.Invoke());
                }
                else
                {
                    //Wrong Answers
                    _correctAnswerButton.GetComponent<AnswerAnimation>().IsCorrectAnswerButton(false);
                    _selectableButtons[i].onClick.AddListener(() => EventsSystem.wrongAnswer.Invoke());
                }
            }
        }
    }
}
