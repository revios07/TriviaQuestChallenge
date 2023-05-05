using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class PlayerDataManagement : JsonReader
    {
        protected JsonDataPlayers[] players;

        protected override void Start()
        {
            base.Start();

            players = new JsonDataPlayers[jsonUrls.Length];

            loadWebRequest = new IEnumerator[jsonUrls.Length];
            jsonTexts = new string[jsonUrls.Length];
            IsLoaded = new bool[jsonUrls.Length];

            for (int i = 0; i < players.Length; ++i)
            {
                loadWebRequest[i] = LoadDataFromWeb<JsonDataPlayers>();
                StartCoroutine(loadWebRequest[i]);
            }
        }
    }
}
