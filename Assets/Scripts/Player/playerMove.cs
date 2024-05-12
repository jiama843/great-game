using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float walkSpeed = 1f;
    public float moveDelay = 0.1f;
    public float moveDistance = 10f;

    public Transform cameraPos;
    public float turnDelay = 0.05f;
    public float turnSpeed = 0.5f;


    //-- Player Input --//
    float forwardInput;
    float turnInput;

    //-- Movement variables --//

    // states if the player is moving or waiting for movement input
    public bool isMoving = false;
 
    // start position before move is executed
    public Vector3 startPos;
 
    // target-position after the move is executed
    public Vector3 endPos;
 
    // stores the progress of the current move in a range from 0f to 1f
    float moveProgress;

    //-- Rotation variables --//
    public bool isTurning = false;
    Quaternion startRot;
    Quaternion targetRot;
    float turnProgress;

    private Rigidbody rb;
    private int layerMask;

    //-- Collision Detection (for walls) --//
    RaycastHit HitInfo;

    private Vector3 playerPosition;
    private Quaternion playerRotation;
    void Awake(){
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        int layerMask = LayerMask.GetMask("Wall");

        if(GamesTracker.itemCompletionStatus.Count > 0)
        {
            playerPosition = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
            playerRotation = new Quaternion(PlayerPrefs.GetFloat("PlayerRotX"), PlayerPrefs.GetFloat("PlayerRotY"), PlayerPrefs.GetFloat("PlayerRotZ"), PlayerPrefs.GetFloat("PlayerRotW"));
            transform.position = playerPosition;
            transform.rotation = playerRotation;
        }
        
    }

    void FixedUpdate()
    {
        forwardInput = Input.GetAxisRaw("Vertical");
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
            // If there is a wall in the direction of move, we skip
            if(blockedByWall()) return;

            // Set state to start move
            startPos = transform.position;

            endPos = startPos + transform.forward * forwardInput * moveDistance;
            isMoving = true;
            moveProgress = 0f;
        }
    }

    private void continueMove(){
        if (moveProgress < 1f)
        {

            moveProgress += Time.deltaTime * walkSpeed;

            // linearly interpolate between our start- and end-positions
            // with the value of our moveProgress which is in range of [0, 1]
            rb.MovePosition(Vector3.Lerp(startPos, endPos, moveProgress));
        }
        else
        {
            isMoving = false;
            rb.position = endPos;
        }
    }

    private bool blockedByWall(){
        bool blockedAhead = Physics.Raycast(transform.position, transform.forward, out HitInfo, 10f) && forwardInput == 1f;
        //bool blockedBehind = Physics.Raycast(transform.position, transform.forward * -1, out HitInfo, 10f) && forwardInput == -1f;
        //debug land
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.red); // Forward raycast
        Debug.DrawRay(transform.position, -transform.forward * 10f, Color.blue); // Backward raycast
        if (blockedAhead && HitInfo.collider.CompareTag("Finish"))
        {
            Debug.Log("Ray check " + HitInfo.collider.name);
            return false;
        }
        
        return blockedAhead;
    }
}
