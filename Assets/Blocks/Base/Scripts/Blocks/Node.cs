using UnityEngine;

namespace Blocks
{
    public class Node : MonoBehaviour
    {
        public Vector3 PlacePosition { get { return m_PlacePosition; } }

        [SerializeField]
        private Vector3 m_PlacePosition;

        private Block m_Block;

        private void Awake()
        {
            //Get the block script.
            m_Block = GetComponentInParent<Block>();
        }

        private void OnMouseEnter() { m_Block.Highlight(); }

        private void OnMouseExit() { m_Block.Normalize(); }
    }
}