using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

namespace PixelCrew
{
    namespace Components
    {
        // Structur for 
        [System.Serializable]
        public struct SpriteAnimationState
        {
            public string name;
            public Sprite[] sprites;
            public UnityEvent onComplite;
            public bool loop;
            public bool allowNext;
        }

        [RequireComponent(typeof(SpriteRenderer))]
        public class SpriteAnimation : MonoBehaviour
        {

            [SerializeField] private int _frameRate;
            [SerializeField] private SpriteAnimationState[] _states;

            private SpriteAnimationState _currentState;
            private Dictionary<string, int> _keyStatesMap;
            private SpriteRenderer _renderer;
            private float _secondsPerFrame;
            private float _nextFrameTime;
            private int _currentSpriteIndex;

            private bool _isPlaying;

            private void Start()
            {
                // I belive that there is more conviniet and smart way to
                // check this list and notice user
                // but i don't know how
                if (_states.Length == 0)
                {
                    _isPlaying = false;
                    //throw new System.ArgumentException("List States cannot be empty");
                }
                else
                {
                    _currentState = _states[0]; // Default state is 0.

                    // Fill map for faster search
                    int i = 0;
                    _keyStatesMap = new Dictionary<string, int>();
                    foreach (var state in _states)
                    {
                        _keyStatesMap.Add(state.name, i++);
                    }
                }

                _renderer = GetComponent<SpriteRenderer>();
                _secondsPerFrame = 1f / _frameRate;
                _nextFrameTime = Time.time;
                _isPlaying = true;
                _currentSpriteIndex = 0;
            }

            private void Update()
            {
                if (_isPlaying && _nextFrameTime < Time.time)
                {
                    UpdateSpriteFrame();
                }
            }

            private void UpdateSpriteFrame()
            {
                _renderer.sprite = _currentState.sprites[_currentSpriteIndex];
                _nextFrameTime += _secondsPerFrame;

                if (++_currentSpriteIndex >= _currentState.sprites.Length)
                {
                    if (_currentState.loop)
                    {
                        _currentSpriteIndex = 0;
                    }
                    else
                    {
                        _isPlaying = false;
                        _currentState.onComplite?.Invoke();
                    }
                }

            }

            public void SetClip(string name)
            {
                int stateIndex = 0;

                if (_currentState.allowNext && _keyStatesMap.TryGetValue(name, out stateIndex)) // Only if "allowNext" flags were set before start
                {
                    _currentState = _states[stateIndex]; // Set new state as current
                    _currentSpriteIndex = 0;
                    _isPlaying = true; // Renew this
                }
            }
        }
    }
}