using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private TeleportComponent _other;

        private Transform _otherTransform;

        private bool _canTeleport;

        public void Awake()
        {
            Enable();
            _otherTransform = _other?.GetTransform();
        }

        public void Teleport(GameObject target)
        {
            if (_otherTransform == null) return;

            if (_canTeleport)
            {
                _other.Disable();
                target.transform.position = _otherTransform.position;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            Enable();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Disable()
        {
            _canTeleport = false;
            GetComponent<Collider2D>().isTrigger = true;
        }

        public void Enable()
        {
            _canTeleport = true;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}