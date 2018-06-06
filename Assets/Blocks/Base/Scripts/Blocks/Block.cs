using UnityEngine;

namespace Blocks
{
    public class Block : MonoBehaviour
    {
        [Header("Materials")]
        [Space(5f)]

        [SerializeField]
        private Material m_Normal;

        [SerializeField]
        private Material m_Highlight;

        [SerializeField]
        private Material m_Transparent;

        private bool m_Placed;
        private MeshRenderer m_Renderer;

        private void Awake()
        {
            //Get the mesh renderer.
            m_Renderer = GetComponent<MeshRenderer>();
        }

        public void Place()
        {
            //Get all the colliders.
            Collider[] colls = transform.GetComponentsInChildren<Collider>();

            //Disable each one.
            for (var i = 0; i < colls.Length; i++)
                colls[i].enabled = true;

            m_Placed = true;

            Normalize();
        }

        public void Highlight()
        {
            //Go back if the block is not placed.
            if (m_Placed == false)
                return;

            ChangeMat(m_Highlight);
        }

        public void Transparent() { ChangeMat(m_Transparent); }

        public void Normalize()
        {
            //Go back if the block is not placed.
            if (m_Placed == false)
                return;

            ChangeMat(m_Normal);
        }

        public void ChangeMat(Material m) { m_Renderer.material = m; }
    }
}