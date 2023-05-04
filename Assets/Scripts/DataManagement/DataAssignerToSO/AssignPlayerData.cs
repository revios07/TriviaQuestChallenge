using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class AssignPlayerData : PlayerDataManagement
    {
        private int currentPage = 0;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(NameWrite());
        }

        public jsonDataPlayers[] GetPlayers()
        {
            return players;
        }
        public int GetPage()
        {
            return currentPage++;
        }

        private IEnumerator NameWrite()
        {
            for(int i = 0; i < players.Length; ++i)
            {
                yield return new WaitUntil(() => isLoaded[i]);

                players[i] = GetText<jsonDataPlayers>(jsonTexts[i]);

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
