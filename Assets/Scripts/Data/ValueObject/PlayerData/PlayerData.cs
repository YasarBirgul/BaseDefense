using System;

namespace Data.ValueObject.PlayerData
{
    [Serializable]
    public class PlayerData
    {
        public int PlayerHealth;
        public PlayerMovementData PlayerMovementData;
        public float AttackRange;
    }
}