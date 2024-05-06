// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class playerMoveV1 : MonoBehaviour
// {
//     public float walkSpeed = 3f;
//     public float moveDelay = 0.2f;
//     public float moveDistance = 3f;

//     Direction currentDir = Direction.South;
 
//     // a vector storing the input of our input-axis
//     Vector3 input;
 
//     // states if the player is moving or waiting for movement input
//     bool isMoving = false;
 
//     // position before a move is executed
//     Vector3 startPos;
 
//     // target-position after the move is executed
//     Vector3 endPos;
 
//     // stores the progress of the current move in a range from 0f to 1f
//     float progress;

//     float remainingMoveDelay = 0f;

//     private void Start()
//     {
//         //controller = gameObject.AddComponent<CharacterController>();
//     }

//     void Update()
//     {
//         // Try x, y w/ vector2
//         Debug.Log(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
//         Debug.Log(isMoving);
//         //controller.Move(playerVelocity * Time.deltaTime);

//         // check if the player is moving
//         if (!isMoving)
//         {
//             // The player is currently not moving so check if there is keyinput
//             input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
 
//             // if there is input in x direction disable input in y direction to
//             // disable diagonal movement
//             if (input.x != 0f)
//                 input.z = 0;
 
//             // check if there is infact movement or if the input axis are in idle
//             // position
//             if (input != Vector3.zero)
//             {
//                 // save the old direction for later use
//                 Direction oldDirection = currentDir;
 
//                 // update the players direction according to the input
//                 #region update Direction
//                 if (input.x == -1f)
//                     currentDir = Direction.West;
//                 if (input.x == 1f)
//                     currentDir = Direction.East;
//                 if (input.z == 1f)
//                     currentDir = Direction.North;
//                 if (input.z == -1f)
//                     currentDir = Direction.South;
//                 #endregion
 
//                 // if the currentDirection is different from the old direction
//                 // we want to add a delay so the player can just change direction
//                 // without having to move
//                 if (currentDir != oldDirection)
//                 {
//                     remainingMoveDelay = moveDelay;
//                 }
 
//                 // if the direction of the input does not change then the move-
//                 // delay ticks down
//                 if (remainingMoveDelay > 0f)
//                 {
//                     remainingMoveDelay -= Time.deltaTime;
//                     return;
//                 }

//                 startPos = transform.position;
//                 endPos = new Vector3(startPos.x + input.x * moveDistance, startPos.y, startPos.z + input.z * moveDistance);

//                 isMoving = true;
//                 progress = 0f;
//             }
//         }
 
//         // check if the player is currently in the moving state
//         if (isMoving)
//         {
//             // check if the progress is still below 1f so the movement is still
//             // going on
//             if (progress < 1f)
//             {
//                 // increase our movement progress by our deltaTime times our
//                 // above specified walkspeed
//                 progress += Time.deltaTime * walkSpeed;
 
//                 // linearly interpolate between our start- and end-positions
//                 // with the value of our progress which is in range of [0, 1]
//                 transform.position = Vector3.Lerp(startPos, endPos, progress);
//             }
//             else
//             {
//                 // if we are moving and our progress is above one that means we
//                 // either landed exactly on our desired position or we overshot
//                 // by some tiny amount so in ordered to not accumulate errors
//                 // we clamp our final position to our desired end-position
//                 isMoving = false;
//                 transform.position = endPos;
//             }
//         }
//     }
// }

// enum Direction
// {
//     North, East, South, West
// }