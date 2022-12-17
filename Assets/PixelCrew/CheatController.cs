using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace PixelCrew
{
    public class CheatController : MonoBehaviour
    {
        private string _currentInput;
        private float _inputTime;

        [SerializeField] private float _inputTimeToLeave;
        [SerializeField] private CheatItem[] _cheats;

        private void Awake()
        {
            Keyboard.current.onTextInput += OnTextInput;
        }

        private void Update()
        {
            if (_inputTime < 0)
            {
                _currentInput = string.Empty;
            }
            else
            {
                _inputTime -= Time.deltaTime;
            }
        }

        private void OnTextInput(char inputChar)
        {
            _currentInput += inputChar;
            _inputTime = _inputTimeToLeave;
            FindAnyCheats();
        }

        private void FindAnyCheats()
        {
            foreach ( var cheatItem in _cheats )
            {
                if(_currentInput.Contains(cheatItem.Name))
                {
                    cheatItem.Action.Invoke();
                    _currentInput = string.Empty;
                }
            }
        }
    }

    [System.Serializable]
    public class CheatItem
    {
        public string Name;
        public UnityEvent Action;
    }
}