using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.DataManagement;

namespace Trivia.Data
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
            yield return new WaitUntil(() => isLoaded);

            players = GetText<jsonDataPlayers>(jsonText);
            Debug.Log(players.data[0].nickname);
        }

        public jsonDataPlayers GetPlayers()
        {
            return players;
        }
    }
}
