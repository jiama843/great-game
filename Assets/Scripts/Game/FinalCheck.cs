using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheck : MonoBehaviour
{
    public playerMove moveState;
    public SwitchScene switchSc;
    bool isInteractable = false;
    bool isFinished = false;
    public TextFade textF;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GamesTracker.itemCompletionStatus.Count == 4)
        {
            isFinished = true;
        }
        if (Input.GetButtonDown("Interact") && !moveState.isTurning && !moveState.isMoving && isInteractable)
        {
            if (isFinished)
            {
                PlayerPrefs.SetInt("NUM_ENCOUNTERS", 0);
                switchSc.LoadNextScene();
            }
            else
            {
                Debug.Log("Job is not done. Pots are in need of fixing and chest are in need of filling.");
                textF.StartTextShow();

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the raycast
        if (other.CompareTag("Player"))
        {
            isInteractable = true;

        }

    }
}
