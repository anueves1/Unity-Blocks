using UnityEngine;

namespace Blocks
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Prefab;

        [Header("Settings")]
        [Space(5f)]

        [SerializeField]
        [Range(0, 1)]
        private float m_HolePossiblity = 0.1f;

        [SerializeField]
        private int m_Rows = 10;

        [SerializeField]
        private int m_Columns = 10;

        private Transform m_Holder;

        private void Start() { Spawn(); }

        private void Update()
        {
            //Check if we need to respawn the floor.
            var refreshFloor = Input.GetKeyDown(KeyCode.R);

            //If we do.
            if (refreshFloor)
            {
                //Destroy the current one.
                DestroyImmediate(m_Holder.gameObject);

                //Spawn a new one.
                Spawn();
            }
        }

        private void MakeHolder()
        {
            //Create a new holder.
            var newHolder = new GameObject("Holder");
            //Save it.
            m_Holder = newHolder.transform;

            //Parent it to this object.
            m_Holder.SetParent(transform);
            //Reset its position.
            m_Holder.localPosition = Vector3.zero;
        }

        public void Spawn()
        {
            //Go back if there's no prefab.
            if (m_Prefab == null)
                return;

            //If we don't have a holder, make one.
            if (m_Holder == null)
                MakeHolder();

            //Go trough the rows.
            for (var y = 0; y < m_Rows; y++)
            {
                //Go trough the columns.
                for (var x = 0; x < m_Columns; x++)
                {
                    //Get the correct position.
                    var pos = new Vector3(x, 0, y);

                    //Get a random number.
                    var spawnNumber = Random.Range(0f, 1f);
                    //If the number is smaller than the hole possiblity, continue without spawning.
                    if (spawnNumber <= m_HolePossiblity)
                        continue;

                    //Spawn the prefab.
                    GameObject nObject = Instantiate(m_Prefab, m_Holder);
                    //Set its position.
                    nObject.transform.position = pos;
                }
            }
        }
    }
}