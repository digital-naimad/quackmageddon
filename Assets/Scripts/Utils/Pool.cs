using UnityEngine;

namespace Quackmageddon
{
    [System.Serializable]
    public struct Pool
    {
        public SpawnType spawnType;
        public GameObject prefab;
        public short poolSize;
    }
}