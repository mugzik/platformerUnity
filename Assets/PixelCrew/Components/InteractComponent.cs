using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;
        
        public void Interact()
        {
            _onInteract?.Invoke();
        }
    }
}