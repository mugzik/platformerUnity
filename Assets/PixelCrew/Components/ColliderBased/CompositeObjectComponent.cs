using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    // Component which provides data related to objects which 
    // Have the same type and are touched to this Collider2D
    public class CompositeObjectComponent : MonoBehaviour
    {
        [SerializeField] private string _typeForComposite;
        [SerializeField] private DynamicEvent _onAdded;
        [SerializeField] private DynamicEvent _onRemoved;

        private List<GameObject> _relatedObjectsList => RelatedObjectsList;

        public string TypeForComposite { get => _typeForComposite; }
        public List<GameObject> RelatedObjectsList { get; private set; }


        private void Awake()
        {
            RelatedObjectsList = new List<GameObject>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var compositeUnit = other.gameObject.GetComponent<CompositeObjectComponent>();

            if (compositeUnit != null && compositeUnit.TypeForComposite == _typeForComposite)
            {
                AddGameObject(other.gameObject);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            var compositeUnit = other.gameObject.GetComponent<CompositeObjectComponent>();

            if (compositeUnit != null && compositeUnit.TypeForComposite == _typeForComposite)
            {
                RemoveGameObject(other.gameObject);
            }
        }

        private void AddGameObject(GameObject go)
        {
            _relatedObjectsList.Add(go);
            Debug.Log("Added " + go.name);
            _onAdded?.Invoke(go);
        }

        private void RemoveGameObject(GameObject go)
        {
            _relatedObjectsList.Remove(go);
            Debug.Log("Removed " + go.name);
            _onRemoved?.Invoke(go);
        }


        [System.Serializable]
        public class DynamicEvent : UnityEvent<GameObject>
        {

        }
    }
}