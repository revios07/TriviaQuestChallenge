using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trivia.Management
{
    public class SceneManagement : MonoBehaviour
    {
        private static SceneManagement instance;
        public static SceneManagement Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject createdGameObject = new GameObject("SceneManagement");
                    createdGameObject.AddComponent<SceneManagement>();
                    return instance;
                }

                return instance;
            }

            private set
            {
                instance = value;
            }
        }

        public int targetLoadLevel { get; private set; } = 1; //Load Default Level First => MainMenu for this


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                return;
            }

            Destroy(gameObject);
        }

        public void LoadLevel(int targetLevel)
        {
            //Level Loader Scene Load
            SceneManager.LoadScene(0);
            targetLoadLevel = targetLevel;
        }
    }
}
