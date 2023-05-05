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
        [SerializeField]
        private TimeSO _timeSO;

        private int _questionIndex = 0;
        private bool _isAllQuestionsComplete;

        private AssignQuestionData _assignQuestionData;

        private Image _questionImage;
        private TMP_Text _questionText;
        private Button[] _selectableButtons;
        private Button _correctAnswerButton;
        private List<TMP_Text> _choicesTextes = new();

        #region Unity Calls
        private void OnEnable()
        {
            EventsSystem.OnNextQuestionLoaded += AssignQuestion;
            EventsSystem.OnPlayerSelectedAnswer += LoadNextQuestion;

            EventsSystem.CorrectAnswer += CorrectAnswerChoose;
            EventsSystem.WrongAnswer += WrongAnswerChoose;
            EventsSystem.NotSelectedAtTime += NotSelectedAtTime;
        }
        private void OnDisable()
        {
            EventsSystem.OnNextQuestionLoaded -= AssignQuestion;
            EventsSystem.OnPlayerSelectedAnswer -= LoadNextQuestion;

            EventsSystem.CorrectAnswer -= CorrectAnswerChoose;
            EventsSystem.WrongAnswer -= WrongAnswerChoose;
            EventsSystem.NotSelectedAtTime += NotSelectedAtTime;
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

            _questionImage = GetComponentInChildren<Image>();
            _questionText = GetComponentInChildren<TMP_Text>();
            _selectableButtons = GetComponentsInChildren<Button>();

        }
        private void Start()
        {
            AssignQuestion();
        }
        #endregion 

        private void LoadNextQuestion()
        {
            Invoke(nameof(AssignQuestion), _timeSO.GetWaitTimeAfterAnswer() + 0.1f);
        }
        private void AssignQuestion()
        {
            if (_isAllQuestionsComplete)
                return;

            if (_questionIndex >= _assignQuestionData.GetQuestions()[0].questions.Count)
            {
                Debug.Log("Question Reached Out!");
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

                    _correctAnswerButton.gameObject.SetActive(false);
                    _correctAnswerButton.gameObject.SetActive(true);

                    _correctAnswerButton.GetComponent<AnswerAnimation>().IsCorrectAnswerButton(true);
                }
                else
                {
                    //Wrong Answers

                    _selectableButtons[i].gameObject.SetActive(false);
                    _selectableButtons[i].gameObject.SetActive(true);

                    _selectableButtons[i].GetComponent<AnswerAnimation>().IsCorrectAnswerButton(false);
                }
            }

            //Next Question
            ++_questionIndex;
        }

        #region Show is Correct Choose to Player
        //I Used this for is play choose correct or false
        private void CorrectAnswerChoose()
        {
            StartCoroutine(AssignQuestionSelection(Color.green));
        }
        private void WrongAnswerChoose()
        {
            StartCoroutine(AssignQuestionSelection(Color.red));
        }
        private void NotSelectedAtTime()
        {
            StartCoroutine(AssignQuestionSelection(Color.blue));
        }

        private IEnumerator AssignQuestionSelection(Color effectColor)
        {
            _questionImage.color = effectColor;

            yield return new WaitForSeconds(_timeSO.GetWaitTimeAfterAnswer() - 0.1f);

            _questionImage.color = Color.white;
        }
        #endregion
    }
}
