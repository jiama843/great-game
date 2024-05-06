using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float moveDelay = 0.1f;
    public float moveDistance = 50f;

    public Transform cameraPos;
    public float turnDelay = 0.1f;
    public float turnSpeed = 0.5f;

    // Movement variables //
    Direction currentDir = Direction.Forward;
 
    // states if the player is moving or waiting for movement input
    bool isMoving = false;

    // player input
    float forwardInput;
    float turnInput;
 
    // keep track of player input
    Vector3 startPos;
 
    // target-position after the move is executed
    Vector3 endPos;
 
    // stores the progress of the current move in a range from 0f to 1f
    float moveProgress;

    float remainingMoveDelay = 0f;

    // Rotation variables //
    // Rotation currentRot;
    bool isTurning = false;
    Quaternion startRot;
    Quaternion targetRot;
    float turnProgress;
    // float remainingTurnDelay = 0f;

    private void Start()
    {
        //controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        // Try x, y w/ vector2
        // Debug.Log(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
        // Debug.Log(isMoving);
        //controller.Move(playerVelocity * Time.deltaTime);

        forwardInput =  Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        // TODO: Replace with state machine w/ 2 states (move, rotate);
        if (!isTurning) handleMove();
        if (!isMoving) handleTurn();
    }

    private void handleTurn(){
        if (!isTurning)
        {
            startTurn();
        }
        else
        {
            continueTurn();
        }
    }

    private void startTurn(){
        if (turnInput != 0f)
        {
            // IMPORTANT: notice the eulerAngles conversion
            float rotationDegree = transform.rotation.eulerAngles.y;

            #region update Rotation
            if (turnInput == 1f)
                rotationDegree += 90f;
            if (turnInput == -1f)
                rotationDegree -= 90f;
            #endregion

            // Set state to start rotation
            isTurning = true;
            startRot = transform.rotation;
            targetRot = Quaternion.Euler(0, rotationDegree, 0);
            turnProgress = 0f;
        }
    }

    private void continueTurn(){
        if (turnProgress < 1f)
        {
            turnProgress += Time.deltaTime * turnSpeed;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, turnProgress);
            cameraPos.rotation = Quaternion.Slerp(startRot, targetRot, turnProgress);
        }
        else
        {
            isTurning = false;
            transform.rotation = targetRot;
            cameraPos.rotation = targetRot;
        }
    }

    private void handleMove(){
        if (!isMoving)
        {
            startMove();
        }
        else
        {
            continueMove();
        }
    }

    private void startMove(){
        if (forwardInput != 0f)
        {
            Direction oldDirection = currentDir;

            #region update Direction
            if (forwardInput == 1f)
                currentDir = Direction.Forward;
            if (forwardInput == -1f)
                currentDir = Direction.Back;
            #endregion

            // Set move delay if the direction of input has changed
            if (currentDir != oldDirection)
            {
                remainingMoveDelay = moveDelay;
            }

            // if the direction of the input does not change then the move-
            // delay ticks down
            if (remainingMoveDelay > 0f)
            {
                remainingMoveDelay -= Time.deltaTime;
                return;
            }

            // Set state to start move
            startPos = transform.position;
            endPos = startPos + transform.forward * forwardInput * moveDistance; // new Vector3(startPos.x, startPos.y, startPos.z + forwardInput * moveDistance);
            isMoving = true;
            moveProgress = 0f;

            Debug.Log(startPos);
            Debug.Log(endPos);
        }
    }

    private void continueMove(){
        if (moveProgress < 1f)
        {
            moveProgress += Time.deltaTime * walkSpeed;

            // linearly interpolate between our start- and end-positions
            // with the value of our moveProgress which is in range of [0, 1]
            transform.position = Vector3.Lerp(startPos, endPos, moveProgress);
        }
        else
        {
            isMoving = false;
            transform.position = endPos;
        }
    }
}

enum Direction
{
    Forward, Back
}