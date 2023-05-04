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
        public delegate void OnNextQuestionLoaded();
        public delegate void OnAllQuestionsComplete();

        //Player Conditions
        public delegate void OnPlayerSelectedAnswer();
        public delegate void OnPlayerSelectCorrect();
        public delegate void OnPlayerNotSelectedAtTime();
        public delegate void OnPlayerSelectWrong();
        #endregion

        #region Events
        public static OnGameStarted onGameStarted;
        public static OnNextQuestionLoaded onNextQuestionLoaded;
        public static OnAllQuestionsComplete onAllQuestionsComplete;

        public static OnPlayerSelectedAnswer onPlayerSelectedAnswer;
        public static OnPlayerSelectCorrect correctAnswer;
        public static OnPlayerNotSelectedAtTime notSelectedAtTime;
        public static OnPlayerSelectWrong wrongAnswer;
        #endregion
    }
}
