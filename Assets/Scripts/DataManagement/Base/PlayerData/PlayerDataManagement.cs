using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;

namespace Trivia.DataManagement
{
    public class PlayerDataManagement : JsonReader
    {
        protected jsonDataPlayers players;

        protected override void Start()
        {
            base.Start();
            players = new jsonDataPlayers();
            loadWebRequest = LoadData<jsonDataPlayers>();
            StartCoroutine(loadWebRequest);
        }
    }
}
