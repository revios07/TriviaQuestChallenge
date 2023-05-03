using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Data
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Score/ScoreData")]
    public class ScoreSO : ScriptableObject
    {
        [SerializeField]
        private int _correctAnswer, _wrongAnswer, _doesntReplyInTime;

        public void GetScores(out int correct, out int wrong, out int doesntReply)
        {
            correct = _correctAnswer;
            wrong = _wrongAnswer;
            doesntReply = _doesntReplyInTime;
        }
    }
}
