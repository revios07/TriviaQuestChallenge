using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Data;
using UnityEngine.Networking;

namespace Trivia.DataManagement
{
    public abstract class JsonReader : MonoBehaviour
    {
        [SerializeField]
        protected string[] jsonURLs;
        protected string[] jsonTexts;

        protected IEnumerator[] loadWebRequest;
        protected bool[] isLoaded;

        protected virtual void Start()
        {

        }

        protected virtual T GetText<T>(string jsonData) where T : class
        {
            T data = JsonUtility.FromJson<T>(jsonData);

            return data;
        }

        protected IEnumerator LoadData<T>() where T : class
        {
            for (int i = 0; i < jsonURLs.Length; ++i)
            {
                var webRequest = new WWW(jsonURLs[i]);

                yield return webRequest;

                Debug.Log("Loaded!");

                if (webRequest.error == null)
                {
                    jsonTexts[i] = webRequest.text;

                    Debug.Log(jsonTexts[i]);

                    isLoaded[i] = true;
                }
                else
                {
                    Debug.LogError("Error!");
                }

                yield return new WaitForSeconds(1f);
            }
        }
    }
}
