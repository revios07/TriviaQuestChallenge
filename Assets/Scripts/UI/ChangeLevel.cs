using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Trivia.Management;

namespace Trivia.UI
{
    public class ChangeLevel : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            SceneManagement.Instance.LoadLevel(levelIndex);
        }
    }
}
