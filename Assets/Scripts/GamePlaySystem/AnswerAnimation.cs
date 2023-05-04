using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AnswerAnimation : MonoBehaviour
    {
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Button _answerButton;
        private Image _buttonVisual;

        private bool _isCorrectAnswer;

        #region
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(SelectedAnswer);
        }
        private void OnDisable()
        {
            if (_isCorrectAnswer)
            {
                EventsSystem.notSelectedAtTime -= PlayCorrectAnimation;
                EventsSystem.onPlayerSelectedAnswer -= PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.notSelectedAtTime -= PlayWrongAnimation;
                EventsSystem.onPlayerSelectedAnswer -= PlayWrongAnimation;
            }

            GetComponent<Button>().onClick.RemoveAllListeners();
        }
        private void Awake()
        {
            _answerButton = GetComponent<Button>();
            _buttonVisual = GetComponent<Image>();
        }
        private void Start()
        {

        }
        #endregion

        public void SelectedAnswer()
        {
            EventsSystem.onPlayerSelectedAnswer?.Invoke();

            if (_isCorrectAnswer)
            {
                EventsSystem.correctAnswer?.Invoke();
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.wrongAnswer?.Invoke();
            }
        }

        #region Animation Calls
        private void PlayCorrectAnimation()
        {
            StartCoroutine(AnimationCorrectChoise());
        }
        private void PlayWrongAnimation()
        {
            StartCoroutine(AnimationWrongChoise());
        }
        #endregion

        #region Animations
        private IEnumerator AnimationCorrectChoise()
        {
            _answerButton.interactable = false;

            yield return _waitForFixedUpdate;

            _buttonVisual.color = Color.green;

            yield return new WaitForSeconds(3f);

            _buttonVisual.color = Color.white;
            _answerButton.interactable = true;
        }
        private IEnumerator AnimationWrongChoise()
        {
            _answerButton.interactable = false;

            yield return new WaitForFixedUpdate();

            GetComponent<Image>().color = Color.red;

            yield return new WaitForSeconds(3f);

            GetComponent<Image>().color = Color.white;
            _answerButton.interactable = true;
        }
        #endregion

        public void IsCorrectAnswerButton(bool isCorrect)
        {
            _isCorrectAnswer = isCorrect;

            if (_isCorrectAnswer)
            {
                EventsSystem.notSelectedAtTime += PlayCorrectAnimation;
                EventsSystem.onPlayerSelectedAnswer += PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.notSelectedAtTime += PlayWrongAnimation;
                EventsSystem.onPlayerSelectedAnswer += PlayWrongAnimation;
            }
        }

        private void CheckCalls()
        {
            
        }
        private void CloseCanClickable()
        {
            
        }
    }
}
