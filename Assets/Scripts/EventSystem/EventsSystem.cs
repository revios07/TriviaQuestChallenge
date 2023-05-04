using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trivia.Management
{
    public class EventsSystem : MonoBehaviour
    {
        #region Delegates
        //Game Base Delegates
        public delegate void DL_OnGameStarted();
        public delegate void DL_OnNextQuestionLoaded();
        public delegate void DL_OnAllQuestionsComplete();

        //Player Condition Delegates
        public delegate void DL_OnPlayerSelectedAnswer();
        public delegate void DL_OnPlayerSelectCorrect();
        public delegate void DL_OnPlayerNotSelectedAtTime();
        public delegate void DL_OnPlayerSelectWrong();
        #endregion

        #region Events
        //Game Base Events
        public static DL_OnGameStarted OnGameStarted { get; set; }
        public static DL_OnNextQuestionLoaded OnNextQuestionLoaded { get; set; }
        public static DL_OnAllQuestionsComplete OnAllQuestionsComplete { get; set; }

        //Player Condition Events
        public static DL_OnPlayerSelectedAnswer OnPlayerSelectedAnswer { get; set; }
        public static DL_OnPlayerSelectCorrect CorrectAnswer { get; set; }
        public static DL_OnPlayerNotSelectedAtTime NotSelectedAtTime { get; set; }
        public static DL_OnPlayerSelectWrong WrongAnswer { get; set; }
        #endregion
    }
}
