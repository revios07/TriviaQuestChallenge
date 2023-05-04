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
        private bool _isAllQuestionsComplete;

        private AssignQuestionData _assignQuestionData;

        private TMP_Text _questionText;
        private Button[] _selectableButtons;
        private Button _correctAnswerButton;
        private List<TMP_Text> _choicesTextes = new();

        #region Unity Calls
        private void OnEnable()
        {
            EventsSystem.OnNextQuestionLoaded += AssignQuestion;
            EventsSystem.OnPlayerSelectedAnswer += LoadNextQuestion;
        }
        private void OnDisable()
        {
            EventsSystem.OnNextQuestionLoaded -= AssignQuestion;
            EventsSystem.OnPlayerSelectedAnswer -= LoadNextQuestion;
        }
        private void Awake()
        {
            try
            {
                _assignQuestionData = FindObjectOfType<AssignQuestionData>();
            }
            catch
            {
                Debug.Log("Not Find Question Assigner!");
            }

            _questionText = GetComponentInChildren<TMP_Text>();
            _selectableButtons = GetComponentsInChildren<Button>();

        }
        private void Start()
        {
            LoadNextQuestion();
        }
        #endregion 

        private void LoadNextQuestion()
        {
            Invoke(nameof(AssignQuestion), 3f);
        }
        private void AssignQuestion()
        {
            if (_isAllQuestionsComplete)
                return;

            if (_questionIndex >= _assignQuestionData.GetQuestions()[0].questions.Count)
            {
                Debug.Log("Question Reached Out!");
                Debug.LogError("Turn To First Question!");
                _questionIndex = 0;
                _isAllQuestionsComplete = true;
                EventsSystem.OnAllQuestionsComplete?.Invoke();
                return;
            }

            foreach (var transform in _selectableButtons)
            {
                _choicesTextes.Add(transform.GetComponentInChildren<TMP_Text>());
            }

            var question = _assignQuestionData.GetQuestions()[0].questions[_questionIndex];
            Debug.Log("Answer : " + _assignQuestionData.GetQuestions()[0].questions[_questionIndex].answer);

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
                }
                else
                {
                    //Wrong Answers
                    _selectableButtons[i].GetComponent<AnswerAnimation>().IsCorrectAnswerButton(false);
                }
            }

            //Next Question
            ++_questionIndex;
        }
    }
}
