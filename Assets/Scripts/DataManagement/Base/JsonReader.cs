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
        protected string[] jsonUrls;
        protected string[] jsonTexts;

        protected IEnumerator[] loadWebRequest;
        public bool[] isLoaded { get; protected set; }

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
            for (int i = 0; i < jsonUrls.Length; ++i)
            {
                var webRequest = new WWW(jsonUrls[i]);

                yield return webRequest;

                if (webRequest.error == null)
                {
                    jsonTexts[i] = webRequest.text;

                    Debug.Log("Loaded!" + typeof(T) + "" + i + "");
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
