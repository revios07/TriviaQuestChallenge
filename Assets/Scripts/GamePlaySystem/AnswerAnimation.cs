using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.Management;

namespace Trivia.GamePlay
{
    public class AnswerAnimation : MonoBehaviour
    {
        [SerializeField]
        private Trivia.Data.TimeSO _timeSO;

        private bool _isCorrectAnswer;
        private bool _isGameEnded;

        private readonly WaitForFixedUpdate _waitForFixedUpdate = new();
        private Button _answerButton;
        private Image _buttonVisual;

        #region
        private void OnEnable()
        {
            GetComponent<Button>().onClick.AddListener(SelectedAnswer);
            EventsSystem.OnAllQuestionsComplete += GameEnded;
        }
        private void OnDisable()
        {
            EventsSystem.OnAllQuestionsComplete -= GameEnded;

            if (_isCorrectAnswer)
            {
                EventsSystem.NotSelectedAtTime -= PlayCorrectAnimation;
                EventsSystem.OnPlayerSelectedAnswer -= PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.NotSelectedAtTime -= PlayWrongAnimation;
                EventsSystem.OnPlayerSelectedAnswer -= PlayWrongAnimation;
            }

            GetComponent<Button>().onClick.RemoveAllListeners();
        }
        private void Awake()
        {
            _answerButton = GetComponent<Button>();
            _buttonVisual = GetComponent<Image>();
        }
        #endregion

        public void SelectedAnswer()
        {
            EventsSystem.OnPlayerSelectedAnswer?.Invoke();

            if (_isCorrectAnswer)
            {
                EventsSystem.CorrectAnswer?.Invoke();
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.WrongAnswer?.Invoke();
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
            _buttonVisual.color = Color.green;

            var animTimer = _timeSO.GetWaitTimeAfterAnswer();
            var isScaleToUpper = true;
            var animationStartScale = transform.localScale;

            //Button Move Animation
            while (animTimer > 0.0f)
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
            _buttonVisual.color = Color.red;

            var animTimer = _timeSO.GetWaitTimeAfterAnswer();
            var isScaleToUpper = false;
            var animationStartScale = transform.localScale;

            //Button Move Animation
            while (animTimer > 0.0f)
            {
                yield return _waitForFixedUpdate;

                animTimer -= Time.fixedDeltaTime;

                ButtonAnimationEffect(ref isScaleToUpper, ref animTimer, _isCorrectAnswer);
            }

            yield return _waitForFixedUpdate;

            //Load Defaults
            LoadDefaultButtonProperties(animationStartScale);
        }
        #endregion

        #region Animation Effect
        [Tooltip("Use With IEnumerator")]
        private void ButtonAnimationEffect(ref bool isScaleToUpper, ref float animTimer, bool isCorrectAnswer)
        {
            if (isScaleToUpper)
            {
                transform.localScale += Vector3.one * 1f * Time.fixedDeltaTime;

                if (animTimer <= (_timeSO.GetWaitTimeAfterAnswer() / 2f) && isCorrectAnswer)
                {
                    isScaleToUpper = false;
                }
            }
            else if (!isScaleToUpper)
            {
                transform.localScale -= Vector3.one * 1f * Time.fixedDeltaTime;

                if (animTimer <= (_timeSO.GetWaitTimeAfterAnswer() / 2f) && !isCorrectAnswer)
                {
                    isScaleToUpper = true;
                }
            }
        }
        private void LoadDefaultButtonProperties(Vector3 animationStartScale)
        {
            if (_isGameEnded)
                return;

            transform.localScale = animationStartScale;
            _buttonVisual.color = Color.white;
            _answerButton.interactable = true;
        }
        #endregion

        public void IsCorrectAnswerButton(bool isCorrect)
        {
            _isCorrectAnswer = isCorrect;

            if (_isCorrectAnswer)
            {
                EventsSystem.NotSelectedAtTime += PlayCorrectAnimation;
                EventsSystem.OnPlayerSelectedAnswer += PlayCorrectAnimation;
            }
            else if (!_isCorrectAnswer)
            {
                EventsSystem.NotSelectedAtTime += PlayWrongAnimation;
                EventsSystem.OnPlayerSelectedAnswer += PlayWrongAnimation;
            }
        }
        private void GameEnded()
        {
            _isGameEnded = true;
            _answerButton.interactable = false;
        }
    }
}
