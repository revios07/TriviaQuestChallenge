using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Trivia.Data;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class ScoreHolder : MonoBehaviour
    {
        [SerializeField]
        private ScoreSO _scoreSO;

        private TMP_Text _scoreText;
        private int _correctAnswerScore, _wrongAnswerScore, _notAnswered;
        private int _currentScore = 0;

        #region Unity Calls
        private void OnEnable()
        {
            _scoreSO.GetScores(out _correctAnswerScore, out _wrongAnswerScore, out _notAnswered);

            EventsSystem.CorrectAnswer += CorrectAnswer;
            EventsSystem.WrongAnswer += WrongAnswer;
            EventsSystem.NotSelectedAtTime += NotSelected;
        }
        private void OnDisable()
        {
            EventsSystem.CorrectAnswer -= CorrectAnswer;
            EventsSystem.WrongAnswer -= WrongAnswer;
            EventsSystem.NotSelectedAtTime -= NotSelected;
        }
        private void Start()
        {
            _scoreText = GetComponent<TMP_Text>();
        }
        #endregion

        #region Event Callers
        private void CorrectAnswer()
        {
            _currentScore += _correctAnswerScore;
            WriteTextToScreen();
            CheckHighScore();
        }
        private void WrongAnswer()
        {
            _currentScore += _wrongAnswerScore;
            WriteTextToScreen();
        }
        private void NotSelected()
        {
            _currentScore += _notAnswered;
            WriteTextToScreen();
        }
        private void WriteTextToScreen()
        {
            _scoreText.text = "Score : " + _currentScore;
        }
        private void CheckHighScore()
        {
            _scoreSO.SetHighScore(_currentScore);
        }
        #endregion
    }
}
