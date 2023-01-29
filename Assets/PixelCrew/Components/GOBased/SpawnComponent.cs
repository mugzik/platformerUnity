using System.Collections;
using UnityEngine;

namespace PixelCrew.Components.GOBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefub;
        [SerializeField] private bool _isChildOfParent = false;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            GameObject instance;
            if (!_isChildOfParent)
            {
                instance = Instantiate(_prefub, _target.position, Quaternion.identity);
                instance.transform.localScale = _target.lossyScale;
            }
            else
                instance = Instantiate(_prefub, _target.position, Quaternion.identity, _target);

        }
    }
}