using UnityEngine;

namespace Blocks
{
    public class Node : MonoBehaviour
    {
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