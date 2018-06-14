using Unity.Entities;
using UnityEngine;

namespace Anueves1.Blocks
{
    public class MapSpawningSystem : ComponentSystem
    {
        private struct Filter
        {
            public MapSpawnerComponent SpawnerComponent;
        }

        protected override void OnStartRunning()
        {
            //Go trough the entities.
            foreach (var entity in GetEntities<Filter>())
                Spawn(entity.SpawnerComponent);
        }

        protected override void OnUpdate()
        {
            //Go trough the entities.
            foreach (var entity in GetEntities<Filter>())
            {
                //Check if it needs to be rebuilt.
                if (!entity.SpawnerComponent.Rebuild) 
                    continue;
                
                //Rebuild it.
                Rebuild(entity.SpawnerComponent);
                    
                //Stop rebuilding.
                entity.SpawnerComponent.Rebuild = false;
            }
        }

        private static void Spawn(MapSpawnerComponent component)
        {
            //Go back if there's no prefab.
            if (component.Prefab == null)
                return;

            //If we don't have a holder, make one.
            if (component.Holder == null)
                MakeHolder(component);

            //Go trough the rows.
            for (var y = 0; y < component.Rows; y++)
            {
                //Go trough the columns.
                for (var x = 0; x < component.Columns; x++)
                {
                    //Get the correct position.
                    var pos = new Vector3(x, 0, y);

                    //Get a random number.
                    var spawnNumber = Random.Range(0f, 1f);
                    //If the number is smaller than the hole possiblity, continue without spawning.
                    if (spawnNumber <= component.HolePossibility)
                        continue;

                    //Spawn the prefab.
                    var nObject = Object.Instantiate(component.Prefab, component.Holder);
                    //Set its position.
                    nObject.transform.position = pos;

                    //Get the block.
                    var block = nObject.GetComponent<BlockComponent>();
                    
                    //Place it.
                    block.Place();
                }
            }
        }
        
        public static void Rebuild(MapSpawnerComponent component)
        {
            //Destroy the current one.
            Object.DestroyImmediate(component.Holder.gameObject);

            //Spawn a new one.
            Spawn(component);
        }
        
        private static void MakeHolder(MapSpawnerComponent component)
        {
            //Create a new holder.
            var newHolder = new GameObject("Holder");
            //Save it.
            component.Holder = newHolder.transform;

            //Parent it to this object.
            component.Holder.SetParent(component.transform);
            //Reset its position.
            component.Holder.localPosition = Vector3.zero;
        }
    }
}