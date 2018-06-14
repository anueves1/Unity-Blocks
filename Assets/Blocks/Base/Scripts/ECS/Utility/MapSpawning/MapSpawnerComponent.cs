using UnityEngine;

namespace Anueves1.Blocks
{
    public class MapSpawnerComponent : MonoBehaviour
    {
        public GameObject Prefab;

        [Header("Settings")] 
        [Space(5f)] 
        
        [Range(0, 1)]
        public float HolePossibility = 0.3f;

        public int Rows = 50;

        public int Columns = 50;

        [HideInInspector] 
        public Transform Holder;

        [HideInInspector] 
        public bool Rebuild;

        public void RebuildMap() => Rebuild = true;
    }
}