using Unity.Entities;
using UnityEngine;

namespace Anueves1.Blocks
{
    public class PlayerBuildingSystem : ComponentSystem
    {
        private struct Filter
        {
            public PlayerBuildingComponent BuildingComponent;
        }
        
        protected override void OnUpdate()
        {
            //Go trough the entities.
            foreach (var entity in GetEntities<Filter>())
            {
                //Get the building component.
                var pBuilding = entity.BuildingComponent;
                
                //Change placement state based on key.
                if (Input.GetKeyDown(KeyCode.B))
                    ChangeMode(pBuilding, 1);
                else if (Input.GetKeyDown(KeyCode.R))
                    ChangeMode(pBuilding, 2);

                HandleRaycast(pBuilding);
            }
        }

        private static void HandleRaycast(PlayerBuildingComponent component)
        {
            //Go back if we can't place anything.
            if (component.Mode == 0)
                return;

            //Calculate the ray.
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Choose which mask to use.
            var maskToUse = component.Mode == 1 ? component.NodeMask : component.NoNodeMask;
            
            //Raycast from the center of the camera.
            if (Physics.Raycast(ray, out var hit, component.BuildDistance, maskToUse))
            {
                //If we hit a node.
                var node = hit.transform.GetComponent<NodeComponent>();
                if (component.Mode == 1 && node != null)
                {
                    //Get the block.
                    var block = node.GetComponentInParent<BlockComponent>();

                    //Check if we're still hitting the same object or not.
                    if (block != component.CurrentSelected)
                    {
                        if(component.CurrentSelected)
                            //Deselect the last object.
                            component.CurrentSelected.ChangeMat(0);

                        //Get the new one.
                        component.CurrentSelected = block;
                    }
                    
                    //If there's no object.
                    if (component.Preview == null)
                        SpawnSelected(component);

                    //Get the position where it should be placed.
                    var placePos = component.CurrentSelected.transform.position + node.PlacePosition;

                    //Place it.
                    if (component.Preview != null)
                        //Move it.
                        component.Preview.transform.position = placePos;

                    //Change its material.
                    component.CurrentSelected.ChangeMat(1);
                    
                    //Place if we left click.
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                        Place(component);
                }
                else if(component.Mode == 2)
                {
                    //Get the block.
                    var block = hit.transform.GetComponent<BlockComponent>();
                    
                    //Check if we're still hitting the same object or not.
                    if (block != component.CurrentSelected)
                    {
                        if(component.CurrentSelected)
                            //Deselect the last object.
                            component.CurrentSelected.ChangeMat(0);

                        //Get the new one.
                        component.CurrentSelected = block;
                    }
                    
                    //Change its material to destroy material.
                    block.ChangeMat(3);

                    //If we left click, destroy it.
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                        Object.DestroyImmediate(block.gameObject);
                }
                else
                    DestroyPreview(component);
            }
            else
                DestroyPreview(component);
        }

        private static void DestroyPreview(PlayerBuildingComponent component)
        {
            //If we had an object selected.
            if (component.CurrentSelected)
            {
                //Deselect it.
                component.CurrentSelected.ChangeMat(0);

                //Lose the reference.
                component.CurrentSelected = null;
            }
                
            //Destroy our preview.
            Object.DestroyImmediate(component.Preview);
        }
        
        private static void Place(PlayerBuildingComponent component)
        {
            //Unparent.
            component.Preview.transform.SetParent(null);

            //Place the block.
            component.Preview.GetComponent<BlockComponent>().Place();

            //Lose the reference.
            component.Preview = null;
            
            //Change the material to normal.
            component.CurrentSelected.ChangeMat(0);

            //Deselect the object.
            component.CurrentSelected = null;
        }

        private static void SpawnSelected(PlayerBuildingComponent component)
        {
            //Make an object from the prefab.
            component.Preview = Object.Instantiate(component.Prefab, component.transform);

            //Get all the colliders.
            var colls = component.Preview.GetComponentsInChildren<Collider>();

            //Disable each one.
            foreach (var t in colls)
                t.enabled = false;

            //Make the block have the placing material.
            component.Preview.GetComponent<BlockComponent>().ChangeMat(2);
        }

        private static void ChangeMode(PlayerBuildingComponent component, int id)
        {
            //Destroy the object.
            Object.DestroyImmediate(component.Preview);

            //Change the id.
            component.Mode = id;
        }
    }
}