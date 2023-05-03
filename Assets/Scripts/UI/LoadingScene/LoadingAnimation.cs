using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trivia.Interfaces;
using UnityEngine.SceneManagement;
using Trivia.Management;

namespace Trivia.UI
{
    public class LoadingAnimation : MonoBehaviour, ICanAnimate
    {
        [SerializeField]
        private float _minLoadTime = 1f;
        private int targetLevel = 0;

        private Slider _loadingSlider;
        private TMP_Text _loadingText;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        [SerializeField]
        private float _animationTimerLoading = 1f;
        private string[] _loadingTexts = new string[] { "Loading.", "Loading..", "Loading..." };

        #region Unity CallBacks
        private void Awake()
        {
            _loadingSlider = GetComponentInChildren<Slider>();
            _loadingText = GetComponentInChildren<TMP_Text>();
        }

        private void Start()
        {
            targetLevel = SceneManagement.Instance.targetLoadLevel;

            if (targetLevel != SceneManager.GetActiveScene().buildIndex)
                StartAnimation();
        }
        #endregion

        #region Interface Implementation
        public void StartAnimation()
        {
            StartCoroutine(LoadingAnimations(targetLevel));
        }

        public void StopAnimation()
        {

        }
        #endregion

        private IEnumerator LoadingAnimations(int targetLevel)
        {
            yield return _waitForFixedUpdate;

            int buildIndex = targetLevel;
            AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);
            operation.allowSceneActivation = false;

            float progress = 0f;
            float timer = _minLoadTime;
            int counter = 0;

            while (!operation.isDone)
            {
                yield return _waitForFixedUpdate;

                timer -= Time.fixedDeltaTime;
                progress += (Time.unscaledDeltaTime) / (_minLoadTime);
                _loadingSlider.value = progress;

                //Loading Text Animation
                _loadingText.text = _loadingTexts[counter];

                _animationTimerLoading += 10f * Time.fixedDeltaTime;
                if (_animationTimerLoading > 1f)
                {
                    ++counter;
                    _animationTimerLoading = 0f;

                    if (counter >= _loadingTexts.Length)
                        counter = 0;
                }
                //---

                if (timer <= 0f)
                {
                    Time.timeScale = 1f;
                    operation.allowSceneActivation = true;
                    break;
                }
            }

            if (_loadingSlider.value < 0.99f)
            {
                _loadingSlider.value = 1f;
            }

            yield return new WaitForSeconds(0.01f);
            operation.allowSceneActivation = true;
        }
    }
}
