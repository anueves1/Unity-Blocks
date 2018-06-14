using UnityEngine;

namespace Anueves1.Blocks
{
    public class PlayerBuildingComponent : MonoBehaviour
    {
        public int BuildDistance = 50;

        public GameObject Prefab;
        
        [Header("Masks")]
        [Space(5f)]

        public LayerMask NodeMask;

        public LayerMask NoNodeMask;

        [HideInInspector] 
        public int Mode = 1;

        [HideInInspector] 
        public GameObject Preview;

        [HideInInspector]
        public BlockComponent CurrentSelected;
    }
}