using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Trivia.UI
{
    public class PlayerPanelAssigner : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _rankText, _nameText, _scoreText;

        public void WriteTextes(string playerRank, string playerName, string playerScore)
        {
            _rankText.text = playerRank;
            _nameText.text = playerName;
            _scoreText.text = playerScore;
        }
    }
}
