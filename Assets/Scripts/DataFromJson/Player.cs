using System;
using System.Collections.Generic;

namespace Trivia.Data
{
    [Serializable]
    public class JsonDataPlayers
    {
        public int page;
        public bool is_last;
        public List<Player> data;
    }

    [Serializable]
    public class Player
    {
        public int rank;
        public string nickname;
        public int score;
    }
}
