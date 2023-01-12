using System.Collections;
using UnityEngine;

namespace Assets.PixelCrew.Components
{
    [System.Serializable]
    public struct Loot
    {
        public GameObject lootItem;
        [Range(0f,1f)]public float dropChance; //0 - 1
    }

    public class LootDropComponent : MonoBehaviour
    {
        [SerializeField] private Loot[] _dropList;
        [SerializeField] private int _dropSize;
        [SerializeField] private float _dropOffset;

        public void Drop()
        {
            var offset = 0f;

            for (int i = 0; i < _dropSize; i++)
            {
                var loot = GetRandomLoot();
                if (loot != null)
                {
                    Instantiate(loot, transform.position + new Vector3(offset, 0, 0), Quaternion.identity);
                    offset += _dropOffset;
                }
            }
        }

        private GameObject GetRandomLoot()
        {
            var chance = Random.Range(0, 101)/100f;
            GameObject result = null;

            foreach (var lootItem in _dropList)
            {
                if ( lootItem.dropChance >= chance )
                {
                    result = lootItem.lootItem;
                    break;
                }
                else
                {
                    chance -= lootItem.dropChance;
                }
            }

            return result;
        }
    }
}