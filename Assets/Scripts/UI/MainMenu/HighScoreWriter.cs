using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.UI
{
    public class HighScoreWriter : MonoBehaviour
    {
        [SerializeField]
        private Trivia.Data.ScoreSO _scoreSO;

        private void Start()
        {
            GetComponent<TMPro.TMP_Text>().text = "HighScore => " + _scoreSO.HighScore;
        }
    }
}
