using System;

namespace PixelCrew.Model
{
    [Serializable]
    public class PlayerData : ICloneable
    {
        public int Coins;
        public int HP;
        public bool IsArmed;

        public PlayerData(int coins, int hp, bool isArmed)
        {
            Coins = coins;
            HP = hp;
            IsArmed = isArmed;
        }

        public PlayerData GetCopy()
        {
            return (PlayerData)this.Clone();
        }

        public object Clone()
        {
            return new PlayerData(this.Coins, this.HP, this.IsArmed);
        }
    }
}