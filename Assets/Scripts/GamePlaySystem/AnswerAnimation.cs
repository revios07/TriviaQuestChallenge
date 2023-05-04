using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AnswerAnimation : MonoBehaviour
    {
        private bool _isCorrectAnswer;

        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Button _answerButton;
        private Image _buttonVisual;

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

            var animTimer = 3.0f;
            var isScaleToUpper = true;
            var animationStartScale = transform.localScale;

            //Button Move Animation
            while(animTimer > 0.0f)
            {
                yield return _waitForFixedUpdate;

                animTimer -= Time.fixedDeltaTime;

                ButtonAnimationEffect(ref isScaleToUpper, ref animTimer, _isCorrectAnswer);
            }

            yield return _waitForFixedUpdate;

            //Load Defaults
            LoadDefaultButtonProperties(animationStartScale);
        }
        private IEnumerator AnimationWrongChoise()
        {
            _answerButton.interactable = false;

            yield return new WaitForFixedUpdate();

            _buttonVisual.color = Color.red;

            var animTimer = 3.0f;
            var isScaleToUpper = true;
            var animationStartScale = transform.localScale;

            //Button Move Animation
            while (animTimer > 0.0f)
            {
                yield return _waitForFixedUpdate;

                animTimer -= Time.fixedDeltaTime;

                ButtonAnimationEffect(ref isScaleToUpper, ref animTimer, _isCorrectAnswer);
            }

            yield return new WaitForSeconds(3f);

            //Load Defaults
            LoadDefaultButtonProperties(animationStartScale);
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

        [Tooltip("Use With IEnumerator")]
        private void ButtonAnimationEffect(ref bool isScaleToUpper, ref float animTimer, bool isCorrectAnswer)
        {
            if (isScaleToUpper)
            {
                transform.localScale += Vector3.one * 10f * Time.fixedDeltaTime;

                if (animTimer <= 1.5f && isCorrectAnswer)
                {
                    isScaleToUpper = false;
                }
            }
            else if (!isScaleToUpper)
            {
                transform.localScale -= Vector3.one * 10f * Time.fixedDeltaTime;

                if(animTimer <= 1.5f && !isCorrectAnswer)
                {
                    isScaleToUpper = true;
                }
            }
        }

        private void LoadDefaultButtonProperties(Vector3 animationStartScale)
        {
            transform.localScale = animationStartScale;
            _buttonVisual.color = Color.white;
            _answerButton.interactable = true;
        }

        private void CheckCalls()
        {
            
        }
        private void CloseCanClickable()
        {
            
        }
    }
}
