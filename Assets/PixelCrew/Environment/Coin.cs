using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Environment
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] protected int _price;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            var coinCounter = collision.GetComponent<CoinCounter>();
            coinCounter?.Count(this);
            coinCounter?.WriteTotal();
        }

        public int getPrice()
        {
            return _price;
        }
    }
}