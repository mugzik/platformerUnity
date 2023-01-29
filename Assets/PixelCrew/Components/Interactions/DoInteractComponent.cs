using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.Interactions
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