using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Management
{
    public class EventsSystem : MonoBehaviour
    {
        #region Delegates
        //Game Base Calls
        public delegate void OnGameStarted();

        //Player Conditions
        public delegate void OnPlayerSelectCorrect();
        public delegate void OnPlayerNotSelectedAtTime();
        public delegate void OnPlayerSelectWrong();
        #endregion

        #region Events
        public static event OnGameStarted onGameStarted;

        public static event OnPlayerSelectCorrect correctAnswer;
        public static event OnPlayerNotSelectedAtTime notSelectedAtTime;
        public static event OnPlayerSelectWrong  wrongAnswer;
        #endregion
    }
}
