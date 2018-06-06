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
        private Material m_Highlighted;

        private MeshRenderer m_Renderer;

        private void Awake()
        {
            //Get the mesh renderer.
            m_Renderer = GetComponent<MeshRenderer>();
        }

        public void Highlight() { ChangeMat(m_Highlighted); }

        public void Normalize() { ChangeMat(m_Normal); }

        public void ChangeMat(Material m) { m_Renderer.material = m; }
    }
}