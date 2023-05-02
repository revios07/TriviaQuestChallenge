using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 100f)]
    private float _leaderBoardMoveSpeed;

    private RectTransform _rectTransform => this.gameObject.transform as RectTransform;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private IEnumerator _changeRectTransformSize;

    private void Start()
    {
        //LeaderBoardCheck(true);
    }

    public void LeaderBoardCheck()
    {
        if (_changeRectTransformSize != null)
            return;

        _changeRectTransformSize = SetRectTransformsSize();
        StartCoroutine(_changeRectTransformSize);

        return;
        //Close LeaderBoard
    }

    private IEnumerator SetRectTransformsSize()
    {
        float refenceResolutionY = transform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
        float target = (_rectTransform.offsetMax.y >= refenceResolutionY / 2f) ? 0f : refenceResolutionY;

        Debug.Log("Target : " + target);
        Debug.Log("Target Now : " + _rectTransform.offsetMax.y);


        while (true)
        {
            yield return _waitForFixedUpdate;

            Debug.Log(_rectTransform.position.y);

            if (_rectTransform.offsetMax.y < 1920f)
            {
                _rectTransform.position += Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                if (_rectTransform.position.y >= refenceResolutionY / 2f)
                    break;
            }
            else
            {
                _rectTransform.position -= Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                if (_rectTransform.position.y <= 0f)
                    break;
            }
            //_rectTransform.offsetMax = Vector2.Lerp(_rectTransform.offsetMax, new Vector2(_rectTransform.offsetMax.x, 0f), multiplier * _leaderBoardMoveSpeed * Time.fixedDeltaTime);

            Debug.Log((_rectTransform.offsetMax.y));
        }

        _changeRectTransformSize = null;
    }
}
