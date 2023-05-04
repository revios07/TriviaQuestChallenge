using System.Collections;
using System.Collections.Generic;
using Trivia.DataManagement;
using UnityEngine;

namespace Trivia.UI
{
    public class PageChanger : MonoBehaviour
    {
        private AssignPlayerData _assignPlayerData;
        private bool _isPageChanged;
        private int _pageCounter = 0; //Load First Page

        #region Unity Calls
        private void OnEnable()
        {
            _pageCounter = 0;
        }
        private void OnDisable()
        {
            //Close All Panels
            for (int i = 0; i < LeaderBoardController.LeaderBoardPages.Count; ++i)
            {
                LeaderBoardController.LeaderBoardPages[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            //Open First Page Default / On Opened Leader Board Enabled
            LeaderBoardController.LeaderBoardPages[0].transform.GetChild(0).gameObject.SetActive(true);
        }
        private void Awake()
        {
            try
            {
                _assignPlayerData = GameObject.FindObjectOfType<AssignPlayerData>();
            }
            catch
            {
                Debug.Log("Not Finded ! type : " + typeof(AssignPlayerData).Name);
            }
        }
        #endregion

        public void ChangePage()
        {
            if (_pageCounter + 1 > LeaderBoardController.LeaderBoardPages.Count - 1)
            {
                //Load First Page
                _pageCounter = 0;
            }
            else
            {
                //Load Next Page
                ++_pageCounter;
            }

            //Close All Panels
            for (int i = 0; i < LeaderBoardController.LeaderBoardPages.Count; ++i)
            {
                LeaderBoardController.LeaderBoardPages[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            LeaderBoardController.LeaderBoardPages[_pageCounter].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
