using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterController : MonoBehaviour
{
    [Tooltip("This will be 1 / #. Example if given \"10\", player has 1/10 chance of random encounter.")]
    public int chanceDenominator = 7;
    [Tooltip("Since I can't figure out how to save position, just limit encounters so player can freely move through dungeon")]
    public int maxEncounters = 2;

    // Serialized for testing and visibility...
    [SerializeField] int numEncounters = 0;
    SwitchScene switchScene;

    private Vector3 playerPosition;
    private Quaternion playerRotation;

    void Awake()
    {
        switchScene = GetComponent<SwitchScene>();
        numEncounters = PlayerPrefs.GetInt("NUM_ENCOUNTERS", 0);
        
    }

    public void AttemptEncounter()
    {
        Debug.Log("Checking encounter...");
        int numEncounters = PlayerPrefs.GetInt("NUM_ENCOUNTERS", 0);
        Debug.Log("numEncounters" + numEncounters);
        if (numEncounters < maxEncounters && RandChance(chanceDenominator))
        {
            numEncounters++;

            PlayerPrefs.SetInt("NUM_ENCOUNTERS", numEncounters);
            playerPosition = this.transform.position;
            playerRotation = this.transform.rotation;

            PlayerPrefs.SetFloat("PlayerPosX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerPosition.z);

            PlayerPrefs.SetFloat("PlayerRotX", playerRotation.x);
            PlayerPrefs.SetFloat("PlayerRotY", playerRotation.y);
            PlayerPrefs.SetFloat("PlayerRotZ", playerRotation.z);
            PlayerPrefs.SetFloat("PlayerRotW", playerRotation.w);
            Debug.Log("Entering battle!");
            switchScene.LoadNextScene();
        }
    }

    private bool RandChance(int chanceDenominator)
    {
        int ran = UnityEngine.Random.Range(1, chanceDenominator + 1);
        Debug.Log("Random chanse " + ran);
        return  ran == chanceDenominator;
    }
}