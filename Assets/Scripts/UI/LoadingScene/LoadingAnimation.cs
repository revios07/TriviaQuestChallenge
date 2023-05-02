using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trivia.Interfaces;

namespace Trivia.UI
{
    public class LoadingAnimation : MonoBehaviour, ICanAnimate
    {
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private Slider _loadingSlider;
        private TMP_Text _loadingText;

        #region Unity CallBacks
        private void Start()
        {
            StartAnimation();
        }
        #endregion

        #region Interface Implementation
        public void StartAnimation()
        {
            
        }

        public void StopAnimation()
        {
            
        }
        #endregion

        private IEnumerator LoadingAnimations()
        {
            yield return _waitForFixedUpdate;

            while (true)
            {

            }
        }
    }
}
