using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DoInteractComponent : MonoBehaviour
    {

        public void DoInteract(GameObject target)
        {
            var interactableComponent = target.GetComponent<InteractComponent>();

            interactableComponent?.Interact();
        }
    }
}