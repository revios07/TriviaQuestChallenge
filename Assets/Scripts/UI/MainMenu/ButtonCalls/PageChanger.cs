using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.DataManagement;

namespace Trivia.UI
{
    public class PageChanger : MonoBehaviour
    {
        public int PageCounter { get; private set; } = 0; //Load First Page
        private AssignPlayerData _assignPlayerData;

        #region Unity Calls
        private void OnEnable()
        {
            PageCounter = 0;
        }
        private void OnDisable()
        {

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
            if (PageCounter + 1 > LeaderBoardController.LeaderBoardPages.Count - 1)
            {
                //Load First Page
                PageCounter = 0;
            }
            else
            {
                //Load Next Page
                ++PageCounter;
            }

            //Close All Panels
            for (int i = 0; i < LeaderBoardController.LeaderBoardPages.Count; ++i)
            {
                LeaderBoardController.LeaderBoardPages[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            LeaderBoardController.LeaderBoardPages[PageCounter].transform.GetChild(0).gameObject.SetActive(true);
        }

        public void SetCurrentPage(int page)
        {
            PageCounter = page;
        }
    }
}
