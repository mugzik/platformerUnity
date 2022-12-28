using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew
{
    public class CoinCounter : MonoBehaviour
    {
        private int _coinCount = 0;

        [SerializeField] private int _maxCoinsToLose;

        public int GetCoinCount()
        {
            return _coinCount;
        }

        public void Count(Environment.Coin coin)
        {
            _coinCount += coin.getPrice();
        }

        public void WriteTotal()
        {
            Debug.Log( "Coins : " + _coinCount );
        }

        public int LoseCoins()
        {
            // Method trys to lose _maxCoinsToLose coins
            // Return how much coins was lost

            int lostCoinsCount = Mathf.Min(_coinCount, _maxCoinsToLose);
            _coinCount -= lostCoinsCount;

            return lostCoinsCount;
        }
    }
}