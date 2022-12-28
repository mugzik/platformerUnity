using System.Collections;
using UnityEngine;

namespace PixelCrew.Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefub;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance = Instantiate(_prefub, _target.position, Quaternion.identity);
            instance.transform.localScale = _target.lossyScale;
        }
    }
}