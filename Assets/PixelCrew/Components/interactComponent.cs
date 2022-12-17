using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class interactComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onInteract;
        
        public void Interact()
        {
            _onInteract?.Invoke();
        }
    }
}