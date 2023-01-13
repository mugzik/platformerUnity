using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.PixelCrew.Components
{
    public class ExitLevelCompanent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}