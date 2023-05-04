using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AnswerTimer : MonoBehaviour
    {
        [SerializeField]
        private TimeSO _timeData;
        private float _timeForQuestion;
        private bool _isTimeEnded;
        private bool _isGameEnded;

        private TMPro.TMP_Text _timerText;

        #region Unity Calls
        private void OnEnable()
        {
            EventsSystem.OnNextQuestionLoaded += LoadDefaultTime;
            EventsSystem.OnAllQuestionsComplete += GameEnded;
        }
        private void OnDisable()
        {
            EventsSystem.OnNextQuestionLoaded -= LoadDefaultTime;
            EventsSystem.OnAllQuestionsComplete -= GameEnded;
        }
        private void Awake()
        {
            _timerText = GetComponent<TMPro.TMP_Text>();
        }
        private void Start()
        {
            LoadDefaultTime();
        }
        private void Update()
        {
            if (_isTimeEnded || _isGameEnded)
                return;

            if (_timeForQuestion <= 0f)
            {
                //Not Answered At Correct Time
                _timeForQuestion = 0.0f;
                _isTimeEnded = true;

                _timerText.text = _timeForQuestion.ToString();

                EventsSystem.NotSelectedAtTime?.Invoke();
            }
            else if (_timeForQuestion >= 0f)
            {
                _timeForQuestion -= Time.deltaTime;
                _timerText.text = "Timer : " + _timeForQuestion.ToString(".0");

                if (_timeForQuestion <= _timeData.GetAnswerTime() / 3f)
                {
                    _timerText.color = Color.red;
                }
            }
        }
        #endregion
        private void LoadDefaultTime()
        {
            _timeForQuestion = _timeData.GetAnswerTime();
            _timerText.color = Color.white;
            _isTimeEnded = false;
        }
        private void GameEnded()
        {
            _isGameEnded = true;
            _timerText.text = "END!";
        }
    }
}
