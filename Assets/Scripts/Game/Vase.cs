using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vase : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite completeSprite;

    public bool isComplete;
    public bool isInteractable = false;

    public GamesTracker gamesTracker;
    public string itemID;

    public string requiredItem = "";
    public string requiredGameSceneName = "";

    private SpriteRenderer spriteRenderer;

    private Vector3 playerPosition;
    private Quaternion playerRotation;

    public playerMove moveState;
    private GameObject player;
    public GameObject retunPoint;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SpriteCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if(GamesTracker.GetItemCompletionStatus(itemID)) //if minigame complete
        {
            //reset minigame var and complete this item
            isComplete = true;

            

        }
        SpriteCheck();
        if (Input.GetButtonDown("Interact") && !moveState.isTurning && !moveState.isMoving && isInteractable) //&& PlayerPrefs.GetInt(requiredItem, 0) > 0 
        {
            //game starts
            //not complete yet
           
            
            if (GamesTracker.GetItemCompletionStatus(itemID) == false) //if not yet played
            {
                //add to name buffer
                PlayerPrefs.SetString("itemID", itemID);
                //save player pos and rot 
                playerPosition = retunPoint.transform.position;
                playerRotation = player.transform.rotation;

                PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
                PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
                PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

                PlayerPrefs.SetFloat("PlayerRotX", playerRotation.x);
                PlayerPrefs.SetFloat("PlayerRotY", playerRotation.y);
                PlayerPrefs.SetFloat("PlayerRotZ", playerRotation.z);
                PlayerPrefs.SetFloat("PlayerRotW", playerRotation.w);
                //get to scene
                SceneManager.LoadScene(requiredGameSceneName);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the raycast
        if (other.CompareTag("Player"))
        {
            isInteractable = true;
            player = other.gameObject;

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteractable = false;
        }
    }
    void SpriteCheck()
    {
        if (!isComplete)
        {
            spriteRenderer.sprite = defaultSprite;
        }
        else if (isComplete)
        {
            spriteRenderer.sprite = completeSprite;
            isInteractable = false;
        }
    }

    // bool PlayerIsMoving(){
    //     playerMove playerMove = player.GetComponent<playerMove>();
    //     return playerMove.isMoving || playerMove.isTurning;
    // }
}
