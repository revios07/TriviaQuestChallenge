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
        public int highScore { get; private set; }

        public void GetScores(out int correct, out int wrong, out int doesntReply)
        {
            correct = _correctAnswer;
            wrong = _wrongAnswer;
            doesntReply = _doesntReplyInTime;
        }
        public void SetHighScore(int highScore)
        {
            //this.highScore = highScore > this.highScore ? highScore : this.highScore;

            if(highScore > this.highScore)
            {
                this.highScore = highScore;
                Debug.Log("HIGH SCORE!");
            }
        }
    }
}
