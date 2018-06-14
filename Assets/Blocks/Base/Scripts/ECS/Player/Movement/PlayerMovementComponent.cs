using Cinemachine;
using UnityEngine;

namespace Anueves1.Blocks
{
    public class PlayerMovementComponent : MonoBehaviour
    {
        public float Speed = 1;

        [HideInInspector]
        public CinemachineBrain CBrain;
    }
}