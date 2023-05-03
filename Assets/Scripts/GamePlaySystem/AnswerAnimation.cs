using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AnswerAnimation : MonoBehaviour
    {
        private bool _isCorrectAnswer;

        #region
        private void OnEnable()
        {

        }
        private void OnDisable()
        {
            if (_isCorrectAnswer)
            {
                EventsSystem.onPlayerSelectedAnswer -= PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.onPlayerSelectedAnswer -= PlayWrongAnimation;
            }
        }
        private void Awake()
        {

        }
        private void Start()
        {

        }
        #endregion

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
            yield return new WaitForFixedUpdate();
        }
        private IEnumerator AnimationWrongChoise()
        {
            yield return new WaitForFixedUpdate();
        }
        #endregion

        public void IsCorrectAnswerButton(bool isCorrect)
        {
            _isCorrectAnswer = isCorrect;

            if (_isCorrectAnswer)
            {
                EventsSystem.onPlayerSelectedAnswer += PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.onPlayerSelectedAnswer += PlayWrongAnimation;
            }
        }
    }
}
