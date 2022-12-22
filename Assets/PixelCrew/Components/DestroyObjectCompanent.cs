using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Components
{
    public class DestroyObjectCompanent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;

        public void DestroyObject()
        {
            float animationDeley = 0f;
            //if (_destroyAnimation == null)
            //    animationDeley = _destroyAnimation.clip.length;

            Destroy(_objectToDestroy, animationDeley);
        }
    }
}