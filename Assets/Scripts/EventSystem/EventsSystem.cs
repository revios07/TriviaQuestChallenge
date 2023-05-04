using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Management
{
    public class EventsSystem : MonoBehaviour
    {
        #region Delegates
        //Game Base Delegates
        public delegate void OnGameStarted();
        public delegate void OnNextQuestionLoaded();
        public delegate void OnAllQuestionsComplete();

        //Player Condition Delegates
        public delegate void OnPlayerSelectedAnswer();
        public delegate void OnPlayerSelectCorrect();
        public delegate void OnPlayerNotSelectedAtTime();
        public delegate void OnPlayerSelectWrong();
        #endregion

        #region Events
        //Game Base Events
        public static OnGameStarted onGameStarted { get; set; }
        public static OnNextQuestionLoaded onNextQuestionLoaded { get; set; }
        public static OnAllQuestionsComplete onAllQuestionsComplete { get; set; }

        //Player Condition Events
        public static OnPlayerSelectedAnswer onPlayerSelectedAnswer { get; set; }
        public static OnPlayerSelectCorrect correctAnswer { get; set; }
        public static OnPlayerNotSelectedAtTime notSelectedAtTime { get; set; }
        public static OnPlayerSelectWrong wrongAnswer { get; set; }
        #endregion
    }
}
