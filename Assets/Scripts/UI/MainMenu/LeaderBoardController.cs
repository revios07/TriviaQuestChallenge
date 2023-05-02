using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trivia.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LeaderBoardController : MonoBehaviour
    {
        [SerializeField]
        [Range(1f, 100f)]
        private float _leaderBoardMoveSpeed;

        private RectTransform _rectTransform => this.gameObject.transform as RectTransform;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private IEnumerator _changeRectTransformSize;

        //Call For Open or Close LeaderBoard Pop-Up
        public void LeaderBoardCheck()
        {
            if (_changeRectTransformSize != null)
            {
                StopCoroutine(_changeRectTransformSize);
                _changeRectTransformSize = null;
            }

            _changeRectTransformSize = SetRectTransformsSize();
            StartCoroutine(_changeRectTransformSize);

            return;
        }

        //LeaderBoard Pop-Up Animation
        private IEnumerator SetRectTransformsSize()
        {
            float refenceResolutionY = transform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
            float targetPositionY = (_rectTransform.position.y >= -refenceResolutionY / 4f) ? 0f : refenceResolutionY;

            //Check is LeaderBoard Opened?
            bool isOpened = targetPositionY == 0f ? true : false;

            while (true)
            {
                yield return _waitForFixedUpdate;

                //LeaderBoard is Close --> Open LeaderBoard
                if (!isOpened)
                {
                    _rectTransform.position += Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y >= 0f)
                        break;
                }
                //LeaderBoard is Open --> Close LeaderBoard
                else if (isOpened)
                {
                    _rectTransform.position -= Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y <= -refenceResolutionY / 2f)
                        break;
                }
            }

            _changeRectTransformSize = null;
        }
    }
}

