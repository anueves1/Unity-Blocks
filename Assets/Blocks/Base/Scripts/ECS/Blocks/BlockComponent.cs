using UnityEngine;

namespace Anueves1.Blocks
{
    public class BlockComponent : MonoBehaviour
    {
        [Header("Materials")]
        [Space(5f)]

        [SerializeField]
        private Material m_NormalMaterial;

        [SerializeField]
        private Material m_HighlightMaterial;

        [SerializeField]
        private Material m_PlaceMaterial;

        [SerializeField] 
        private Material m_DestroyMaterial;

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
            var colls = transform.GetComponentsInChildren<Collider>();

            //Disable each one.
            foreach (var t in colls)
                t.enabled = true;

            m_Placed = true;

            ChangeMat(0);
        }

        public void ChangeMat(int index)
        {
            Material m = null;

            switch (index)
            {
                case 0:
                    m = m_NormalMaterial;
                    break;
                case 1:
                    m = m_HighlightMaterial;
                    break;
                case 2:
                    m = m_PlaceMaterial;
                    break;
                case 3:
                    m = m_DestroyMaterial;
                    break;
            }
            
            m_Renderer.material = m;
        }
    }
}