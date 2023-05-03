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
        protected string jsonURL;
        protected string jsonText;

        protected IEnumerator loadWebRequest;
        protected bool isLoaded;

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
            var webRequest = new WWW(jsonURL);

            yield return webRequest;

            Debug.Log("Loaded!");

            if (webRequest.error == null)
            {
                jsonText = webRequest.text;

                Debug.Log(jsonText);

                isLoaded = true;
            }
            else
            {
                Debug.LogError("Error!");
            }
        }
    }
}
