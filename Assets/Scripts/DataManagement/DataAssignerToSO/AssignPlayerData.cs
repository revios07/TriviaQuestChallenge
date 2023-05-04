using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class AssignPlayerData : PlayerDataManagement
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(NameWrite());
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

        public jsonDataPlayers[] GetPlayers()
        {
            return players;
        }
    }
}
