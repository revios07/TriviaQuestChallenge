using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trivia.Management
{
    public class SceneManagement : MonoBehaviour
    {
        public int GetSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }
}
