using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew
{
    public class CoinCounter : MonoBehaviour
    {
        private int _coinCount = 0;

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
    }
}