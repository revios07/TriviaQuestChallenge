using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Trivia.DataManagement;
using Trivia.Data;

namespace Trivia.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LeaderBoardController : MonoBehaviour
    {
        public static List<GameObject> LeaderBoardPages { get; set; } = new List<GameObject>();

        [SerializeField]
        [Header("Don't need to assign this!")]
        private int _numberOfPlayerPages = -1;
        [SerializeField]
        [Range(1f, 100f)]
        private float _leaderBoardMoveSpeed;

        private RectTransform _rectTransform => this.gameObject.transform as RectTransform;
        private readonly WaitForFixedUpdate _waitForFixedUpdate = new();
        private IEnumerator _changeRectTransformSize;

        private AssignPlayerData _playerDataAssigner;
        private List<PlayerPanelAssigner> _playerPanelAssigners;
        private GameObject _changePageButtonGO;

        #region Unity Calls
        private void Awake()
        {
            _changePageButtonGO = transform.parent.GetComponentInChildren<PageChanger>().gameObject;
            var openLeaderBoardPages = transform.parent.GetComponentInChildren<LeaderBoardButton>().GetComponent<Button>();
            openLeaderBoardPages.onClick.AddListener(LeaderBoardCheck);

            if (LeaderBoardPages.Count != 0)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                _changePageButtonGO.SetActive(false);
            }

            LeaderBoardPages.Add(this.gameObject);

            try
            {
                _playerDataAssigner = FindObjectOfType<AssignPlayerData>();
            }
            catch
            {
                Debug.LogError("Add Player Data Assigner Prefab to Scene!");
            }
        }
        private void Start()
        {
            StartCoroutine(SetPlayerTextes());
        }
        #endregion

        //Call For Open or Close LeaderBoard Pop-Up
        public void LeaderBoardCheck()
        {
            if (_changeRectTransformSize != null)
            {
                StopCoroutine(_changeRectTransformSize);
                _changeRectTransformSize = null;
            }

            _changeRectTransformSize = SetRectTransformsSize();
            StartCoroutine(_changeRectTransformSize);

            return;
        }

        //LeaderBoard Pop-Up Animation
        private IEnumerator SetRectTransformsSize()
        {
            Debug.Log(transform.position.y);

            float refenceResolutionY = transform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
            float targetPositionY = (_rectTransform.position.y >= 0f) ? -refenceResolutionY / 4.0f : refenceResolutionY / 4.0f;

            //Check is LeaderBoard Opened?
            var isOpened = targetPositionY >= 0f ? false : true;

            while (true)
            {
                yield return _waitForFixedUpdate;

                //LeaderBoard is Close --> Open LeaderBoard
                if (!isOpened)
                {
                    _rectTransform.position += Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y >= 480f)
                    {
                        _changePageButtonGO.SetActive(true);
                        break;
                    }
                }
                //LeaderBoard is Open --> Close LeaderBoard
                else if (isOpened)
                {
                    _rectTransform.position += Vector3.down * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y <= -refenceResolutionY / 4f)
                    {
                        //Load Default Positions Of Players Text
                        _changePageButtonGO.SetActive(false);
                        break;
                    }
                }
            }

            _changeRectTransformSize = null;
        }
        //Wait For Assign The Textes On Leader Board
        private IEnumerator SetPlayerTextes()
        {
            int page = _playerDataAssigner.GetPage();

            //Wait For Load Datas From URL
            yield return new WaitUntil(() => _playerDataAssigner.IsLoaded[page]);
            yield return new WaitForSeconds(0.1f);

            if (_playerDataAssigner.GetPlayers().Length > transform.parent.childCount - 2)
            {
                //Create 1 more page for list players
                GameObject nextPage = Instantiate(this.gameObject, transform.parent);
            }

            _playerPanelAssigners = new List<PlayerPanelAssigner>();
            var counter = 0;
            var jsonDataPlayers = _playerDataAssigner.GetPlayers()[page];

            //Default Write Texter
            _playerPanelAssigners.Add(transform.GetChild(0).GetChild(0).GetComponent<PlayerPanelAssigner>());
            WriteDataToText(_playerPanelAssigners[0], jsonDataPlayers, 0);

            while (transform.GetChild(0).childCount < jsonDataPlayers.data.Count)
            {
                //Create Player Data Text
                GameObject playerTextDataGO = Instantiate(transform.GetChild(0).GetChild(0).gameObject, transform.GetChild(0).transform);
                _playerPanelAssigners.Add(playerTextDataGO.GetComponent<PlayerPanelAssigner>());

                ++counter;
                WriteDataToText(_playerPanelAssigners[counter], jsonDataPlayers, counter);
            }
        }

        private void WriteDataToText(PlayerPanelAssigner playerPanelAssigner, JsonDataPlayers jsonDataPlayers, int index)
        {
            var rank = jsonDataPlayers.data[index].rank;
            var nickName = jsonDataPlayers.data[index].nickname;
            var score = jsonDataPlayers.data[index].score;

            playerPanelAssigner.WriteTextes(rank.ToString(), nickName, score.ToString());
        }
    }
}

