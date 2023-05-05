using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trivia.Data
{
    [CreateAssetMenu(fileName = "TimeData", menuName = "Time/TimeData")]
    public class TimeSO : ScriptableObject
    {
        [SerializeField]
        private float _timeData;
        [SerializeField]
        [Range(1f, 5f)]
        private float _getWaitTimeAfterAnswer = 2f;

        public float GetAnswerTime()
        {
            return _timeData;
        }

        public float GetWaitTimeAfterAnswer()
        {
            return _getWaitTimeAfterAnswer;
        }
    }
}
