using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace PixelCrew
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private CoinsChangeEvent _onCoinsChange;

        private int _coinCount = 0;

        [SerializeField] private int _maxCoinsToLose;

        public int GetCoinCount()
        {
            return _coinCount;
        }

        public void Count(Environment.Coin coin)
        {
            _coinCount += coin.getPrice();
            _onCoinsChange?.Invoke(_coinCount);
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
            _onCoinsChange?.Invoke(_coinCount);

            return lostCoinsCount;
        }

        [System.Serializable]
        public class CoinsChangeEvent : UnityEvent<int>
        {

        }
    }
}