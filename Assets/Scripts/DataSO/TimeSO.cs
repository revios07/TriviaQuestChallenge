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

        public float GetAnswerTime()
        {
            return _timeData;
        }
    }
}
