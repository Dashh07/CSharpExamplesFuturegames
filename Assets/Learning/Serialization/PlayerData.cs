using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Serialization.Save {

    [Serializable]
    public class PlayerData {
        public string playerName = "John Doe";
        public int level = 0;
        public float health = 100;

        public List<string> indexList = new();
    }

    [Serializable]
    public struct EnemyData {
        public string enemyID;
        public int health;
    }
}

