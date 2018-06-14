using UnityEngine;
using Cinemachine;

namespace Blocks
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement")]
        [Space(5f)]

        [SerializeField]
        private float m_Speed;

        private CinemachineBrain m_Camera;

        private void Start()
        {
            //Get the camera's brain.
            m_Camera = transform.GetComponentInChildren<CinemachineBrain>();
        }

        private void FixedUpdate()
        {
            //Get the speed.
            var speed = m_Speed;

            //Check if we need to enable look.
            var lookMode = Input.GetKey(KeyCode.Mouse1);
            //Only enable the brain when we need to look around.
            m_Camera.enabled = lookMode;

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
            Vector3 forward = m_Camera.transform.forward * verticalInput;
            //Compute the sideways movement.
            Vector3 sideways = m_Camera.transform.right * horizontalInput;
            //Compute the up and down movement.
            Vector3 upDown = Vector3.up * upDownInput;

            //Add them to get the movement direction.
            Vector3 dir = (forward + sideways + upDown) * (speed / 10);
               
            //Move towards that direction.
            transform.localPosition += dir;
        }
    }
}