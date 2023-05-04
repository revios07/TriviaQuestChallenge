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
        [SerializeField]
        [Header("Not Sure To Assign Its Check From Data Assigner")]
        private int _numberOfPlayerPages = 1;
        [SerializeField]
        [Range(1f, 100f)]
        private float _leaderBoardMoveSpeed;

        private RectTransform _rectTransform => this.gameObject.transform as RectTransform;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
        private IEnumerator _changeRectTransformSize;

        private AssignPlayerData _playerDataAssigner;
        private List<PlayerPanelAssigner> _playerPanelAssigners;
        private Transform[] _numberOfPages;

        #region Unity Calls
        private void Awake()
        {
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
            float refenceResolutionY = transform.GetComponentInParent<CanvasScaler>().referenceResolution.y;
            float targetPositionY = (_rectTransform.position.y >= -refenceResolutionY / 4f) ? 0f : refenceResolutionY;

            //Check is LeaderBoard Opened?
            var isOpened = targetPositionY == 0f ? true : false;

            while (true)
            {
                yield return _waitForFixedUpdate;

                //LeaderBoard is Close --> Open LeaderBoard
                if (!isOpened)
                {
                    _rectTransform.position += Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y >= 0f)
                        break;
                }
                //LeaderBoard is Open --> Close LeaderBoard
                else if (isOpened)
                {
                    _rectTransform.position -= Vector3.up * 100f * _leaderBoardMoveSpeed * Time.fixedDeltaTime;

                    if (_rectTransform.position.y <= -refenceResolutionY / 2f)
                        break;
                }
            }

            _changeRectTransformSize = null;
        }
        //Wait For Assign The Textes On Leader Board
        private IEnumerator SetPlayerTextes()
        {
            yield return new WaitForSeconds(3f);

            if (_playerDataAssigner.GetPlayers().Length > transform.parent.childCount - 2)
            {
                //Create 1 more page for list players
                Instantiate(this.gameObject, transform.parent);
            }

            _playerPanelAssigners = new List<PlayerPanelAssigner>();
            var counter = 0;
            var jsonDataPlayers = _playerDataAssigner.GetPlayers()[_playerDataAssigner.GetPage()];

            _playerPanelAssigners.Add(transform.GetChild(0).GetComponent<PlayerPanelAssigner>());
            WriteDataToText(_playerPanelAssigners[0], jsonDataPlayers, 0);

            while (transform.childCount < jsonDataPlayers.data.Count)
            {
                //Create Player Data Text
                GameObject playerTextDataGO = Instantiate(transform.GetChild(0).gameObject, transform);
                _playerPanelAssigners.Add(playerTextDataGO.GetComponent<PlayerPanelAssigner>());

                WriteDataToText(_playerPanelAssigners[++counter], jsonDataPlayers, counter);
            }
        }

        private void WriteDataToText(PlayerPanelAssigner playerPanelAssigner, jsonDataPlayers jsonDataPlayers, int index)
        {
            var rank = jsonDataPlayers.data[index].rank;
            var nickName = jsonDataPlayers.data[index].nickname;
            var score = jsonDataPlayers.data[index].score;

            playerPanelAssigner.WriteTextes(rank.ToString(), nickName, score.ToString());
        }
    }
}

