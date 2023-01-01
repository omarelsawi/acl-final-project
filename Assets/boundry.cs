//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class boundry : MonoBehaviour
//{

//    public GameObject Link;
//    //CharacterController cc;


//    private void Awake()
//    {
//        //cc = Link.GetComponent<CharacterController>();
//    }

//    void OnTriggerExit(Collider other)
//    {
//        // Check if the collider that exited the trigger is the player
//        if (other.CompareTag("Player"))
//        {
//            // Get the player's character controller
//            CharacterController cc = other.gameObject.GetComponent<CharacterController>();

//            // Get the player's current position
//            Vector3 position = other.gameObject.transform.position;

//            // Calculate the direction and distance to move the player back within the bounds
//            Vector3 moveDirection = position - this.transform.position;

//            // Invert the x or z component of the moveDirection vector to only move the player in the opposite direction
//            if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.z))
//            {
//                if (moveDirection.x > 0)
//                {
//                    moveDirection.x = -moveDirection.x;
//                }
//            }
//            else
//            {
//                if (moveDirection.z > 0)
//                {
//                    moveDirection.z = -moveDirection.z;
//                }
//            }

//            // Calculate the distance to move the player
//            float moveDistance = moveDirection.magnitude;

//            // Move the player back within the bounds
//            cc.Move(moveDirection.normalized * moveDistance);
//        }
//    }





//}
