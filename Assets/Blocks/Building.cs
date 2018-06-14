using UnityEngine;
using System.Collections.Generic;

/*
namespace UltimateSurvival.Building.NEW
{
    public class Building : MonoBehaviour
    {
        private MeshFilter m_Filter;
        private MeshRenderer m_Renderer;

        private List<Material> m_Materials = new List<Material>();

        private List<MeshFilter> m_Filters = new List<MeshFilter>();
        private List<MeshRenderer> m_Renderers = new List<MeshRenderer>();

        private List<Buildable> m_Buildables = new List<Buildable>();

        private Mesh CreateCombinedMesh()
        {
            List<Material> materials = new List<Material>();
            for (int i = 0; i < m_Renderers.Count; i++)
            {
                Material[] localMaterials = m_Renderers[i].sharedMaterials;
                for (int sM = 0; sM < localMaterials.Length; sM++)
                {
                    Material current = localMaterials[sM];
                    if (materials.Contains(current) == false)
                        materials.Add(current);
                }
            }

            // Each material will have a mesh for it.
            List<Mesh> submeshes = new List<Mesh>();
            for (int i = 0; i < materials.Count; i++)
            {
                Material material = materials[i];

                // Make a combiner for each (sub)mesh that is mapped to the right material.
                List<CombineInstance> combines = new List<CombineInstance>();
                for (int x = 0; x < m_Filters.Count; x++)
                {
                    MeshFilter filter = m_Filters[x];

                    // The filter doesn't know what materials are involved, get the renderer.
                    MeshRenderer renderer = filter.GetComponent<MeshRenderer>();  // <-- (Easy optimization is possible here, give it a try!)

                    //Get the renderer's materials.
                    Material[] rendererMaterials = renderer.sharedMaterials;
                    //Loop trough them.
                    for (int y = 0; y < rendererMaterials.Length; y++)
                    {
                        //Get the current material.
                        Material current = rendererMaterials[y];

                        //Check if the material is instanced.
                        bool isInstanced = current.name.Contains("(Instance)");
                        //Check if we already have it.
                        bool isContained = m_Materials.Contains(current);

                        //If we don't already have this material, add it.
                        if (isContained == false && isInstanced == false)
                            m_Materials.Add(current);
                    }

                    // Let's see if their materials are the one we want right now.
                    Material[] localMaterials = renderer.sharedMaterials;
                    for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
                    {
                        if (localMaterials[materialIndex] != material)
                            continue;
                        // This submesh is the material we're looking for right now.
                        CombineInstance ci = new CombineInstance();
                        ci.mesh = filter.sharedMesh;
                        ci.subMeshIndex = materialIndex;
                        ci.transform = filter.transform.localToWorldMatrix;
                        combines.Add(ci);
                    }
                }
                // Flatten into a single mesh.
                Mesh mesh = new Mesh();
                mesh.CombineMeshes(combines.ToArray(), true);
                submeshes.Add(mesh);
            }

            // The final mesh: combine all the material-specific meshes as independent submeshes.
            List<CombineInstance> finalCombiners = new List<CombineInstance>();
            for (int i = 0; i < submeshes.Count; i++)
            {
                Mesh mesh = submeshes[i];

                CombineInstance ci = new CombineInstance();
                ci.mesh = mesh;
                ci.subMeshIndex = 0;
                ci.transform = Matrix4x4.identity;
                finalCombiners.Add(ci);
            }
            Mesh finalMesh = new Mesh();
            finalMesh.CombineMeshes(finalCombiners.ToArray(), false);

            return finalMesh;
        }

        public void Refresh()
        {
            //Refresh the materials list.
            m_Materials = new List<Material>();

            //Create a combined mesh.
            m_Filter.mesh = CreateCombinedMesh();
            //Assign the neeeded materials.
            m_Renderer.sharedMaterials = m_Materials.ToArray();
            //Activate this gameobject.
            gameObject.SetActive(true);
        }

        private void FindComponents(Buildable buildable)
        {
            //Get all the new mesh filters that we need to add.
            MeshFilter[] newFilters = buildable.GetComponentsInChildren<MeshFilter>();
            //Get all the new mesh renderers.
            MeshRenderer[] newRenderers = buildable.GetComponentsInChildren<MeshRenderer>();

            //Add all the new filters.
            for (int i = 0; i < newFilters.Length; i++)
                m_Filters.Add(newFilters[i]);

            //Add all the new renderers.
            for (int i = 0; i < newRenderers.Length; i++)
            {
                m_Renderers.Add(newRenderers[i]);

                //Disble that renderer.
                newRenderers[i].enabled = false;
            }
        }
            
        public void AddNew(Buildable buildable)
        {
            //If we just created this object.
            if(m_Buildables.Count == 0)
            {
                //Add a mesh filter and a renderer.
                m_Filter = gameObject.AddComponent<MeshFilter>();
                m_Renderer = gameObject.AddComponent<MeshRenderer>();
            }

            //Add the buildable.
            m_Buildables.Add(buildable);

            FindComponents(buildable);

            //Create a new mesh.
            m_Filter.mesh = CreateCombinedMesh();
            //Assign the neeeded materials.
            m_Renderer.sharedMaterials = m_Materials.ToArray();
            //Activate this gameobject.
            gameObject.SetActive(true);
        }

        public void UpdateSockets()
        {
            //Go trough all the buildables and update their sockets.
            for (int i = 0; i < m_Buildables.Count; i++)
                m_Buildables[i].UpdateAllSockets();
        }
    }
}*/