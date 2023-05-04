using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class PlayerDataManagement : JsonReader
    {
        protected jsonDataPlayers[] players;

        protected override void Start()
        {
            base.Start();

            players = new jsonDataPlayers[jsonURLs.Length];

            loadWebRequest = new IEnumerator[jsonURLs.Length];
            jsonTexts = new string[jsonURLs.Length];
            isLoaded = new bool[jsonURLs.Length];

            for (int i = 0; i < players.Length; ++i)
            {
                loadWebRequest[i] = LoadData<jsonDataPlayers>();
                StartCoroutine(loadWebRequest[i]);
            }
        }
    }
}
