using Cinemachine;
using Unity.Entities;
using UnityEngine;

namespace Anueves1.Blocks
{
    public class PlayerMovementSystem : ComponentSystem 
    {
        private struct Filter
        {
            public PlayerMovementComponent MovementComponent;

            public CharacterController Controller;
        }

        protected override void OnStartRunning()
        {
            //Go trough the entities.
            foreach (var entity in GetEntities<Filter>())
            {
                //Get the cinemachine brain component.
                var cBrain = entity.MovementComponent.GetComponentInChildren<CinemachineBrain>();
                
                //Assign it.
                entity.MovementComponent.CBrain = cBrain;
            }
        }

        protected override void OnUpdate()
        {
            //Go trough the entities.
            foreach (var entity in GetEntities<Filter>())
            {
                //Get the speed.
                var speed = entity.MovementComponent.Speed;

                //Store the cBrain in a local variable for easier access.
                var cBrain = entity.MovementComponent.CBrain;

                //Check if we need to enable look.
                var lookMode = Input.GetKey(KeyCode.Mouse1);
                //Only enable the brain when we need to look around.
                cBrain.enabled = lookMode;

                //Check if we need to increse the speed.
                var sprintMode = Input.GetKey(KeyCode.LeftShift);
                //Multiply the speed by 2 when sprinting.
                speed *= sprintMode ? 2 : 1;

                //Get the horizontal input.
                var horizontalInput = Input.GetAxis("Horizontal");
                //Get the vertical input.
                var verticalInput = Input.GetAxis("Vertical");
                //Get the upDown input.
                var upDownInput = Input.GetAxis("UpDown");

                //Compute the forwards movement.
                var forward = cBrain.transform.forward * verticalInput;
                //Compute the sideways movement.
                var sideways = cBrain.transform.right * horizontalInput;
                //Compute the up and down movement.
                var upDown = Vector3.up * upDownInput;

                //Add them to get the movement direction.
                var dir = (forward + sideways + upDown) * (speed / 10);
               
                //Move towards that direction.
                entity.Controller.Move(dir);
            }
        }
    }
}