using System;
using UnityEngine;

namespace PixelCrew.Model
{
    [Serializable]
    public class PlayerData : ICloneable
    {
        public int Coins;
        public int HP;
        public bool IsArmed;
        [Min(1)]public int SwordsCount = 1;

        public PlayerData(int coins, int hp, bool isArmed, int swordsCount)
        {
            Coins = coins;
            HP = hp;
            IsArmed = isArmed;
            SwordsCount = swordsCount;
        }

        public PlayerData GetCopy()
        {
            return (PlayerData)this.Clone();
        }

        public object Clone()
        {
            return new PlayerData(Coins, HP, IsArmed, SwordsCount);
        }
    }
}