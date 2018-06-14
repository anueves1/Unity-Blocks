using UnityEngine;

namespace Blocks
{
    public class Builder : MonoBehaviour
    {
        [Header("Build Settings")]
        [Space(5f)]

        [SerializeField]
        private GameObject m_Prefab;

        [SerializeField]
        private float m_BuildDistance = 20f;

        private int m_Mode;  
        private GameObject m_Object;

        private void Update()
        {
            //Change placement state based on key.
            if (Input.GetKeyDown(KeyCode.B))
                ChangeMode(1);
            else if (Input.GetKeyDown(KeyCode.R))
                ChangeMode(2);
        }

        public void ChangeMode(int id)
        {
            //Destroy the object.
            DestroyImmediate(m_Object);

            //Change the id.
            m_Mode = id;
        }

        private void FixedUpdate()
        {
            //Go back if we can't place anything.
            if (m_Mode == 0)
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //Raycast from the center of the camera.
            if (Physics.Raycast(ray, out hit, m_BuildDistance))
            {
                //If we hit a node.
                if (m_Mode == 1 && hit.transform.GetComponent<Node>())
                {
                    //Get the node.
                    Node node = hit.transform.GetComponent<Node>();

                    //Get the block.
                    Transform blockTransform = node.GetComponentInParent<Block>().transform;

                    //If there's no object.
                    if (m_Object == null)
                        SpawnSelected();

                    //Get the position where it should be placed.
                    Vector3 placePos = blockTransform.position + node.PlacePosition;

                    //Place it.
                    m_Object.transform.position = placePos;

                    //Place if we left click.
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                        Place();
                }
                else if (m_Mode == 2)
                {
                    //Check if we left click.
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        //Get the block.
                        Transform blockTransform = hit.transform.GetComponentInParent<Block>().transform;

                        //Destroy the object.
                        DestroyImmediate(blockTransform.gameObject);
                    }
                }
                else
                    DestroyImmediate(m_Object);
            }
            else
                DestroyImmediate(m_Object);
        }

        private void Place()
        {
            //Unparent.
            m_Object.transform.SetParent(null);

            //Place the block.
            m_Object.GetComponent<Block>().Place();

            //Lose the reference.
            m_Object = null;
        }

        private void SpawnSelected()
        {
            //Make an object from the prefab.
            m_Object = Instantiate(m_Prefab, transform);

            //Get all the colliders.
            Collider[] colls = m_Object.GetComponentsInChildren<Collider>();

            //Disable each one.
            for (var i = 0; i < colls.Length; i++)
                colls[i].enabled = false;

            //Make the block transparent.
            m_Object.GetComponent<Block>().Transparent();
        }
    }
}