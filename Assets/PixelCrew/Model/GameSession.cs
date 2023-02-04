using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace PixelCrew.Model
{
    public class GameSession : MonoBehaviour 
    {
        [SerializeField] private PlayerData _data;

        private PlayerData _startLevelData;
        private string _previousLoadedScene;
        private bool _isValid = true;

        public PlayerData Data => _data;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (IsSessionExist())
            {
                _isValid = false;
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(this);
            }
        }

        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {

            if (!_isValid) return;

            if (_previousLoadedScene == null || scene.name != _previousLoadedScene )
            {
                SaveStartLevelData();
            }
            else
            {
                ReloadData();
            }

            _previousLoadedScene = scene.name;
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();

            foreach ( var session in sessions )
            {
                if (session != this)
                {
                    return true;
                }
            }

            return false;
        }

        public void ReloadData()
        {
            if (_startLevelData != null)
            {
                _data.Coins = _startLevelData.Coins;
                _data.HP = _startLevelData.HP;
                _data.IsArmed = _startLevelData.IsArmed;
                _data.SwordsCount = _startLevelData.SwordsCount;
            }
        }

        public void SaveStartLevelData()
        {
            _startLevelData = _data.GetCopy();
        }
    }
}