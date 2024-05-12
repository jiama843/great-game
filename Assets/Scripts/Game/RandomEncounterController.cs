using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounterController : MonoBehaviour
{
    [Tooltip("This will be 1 / #. Example if given \"10\", player has 1/10 chance of random encounter.")]
    [SerializeField] int chanceDenominator = 7;
    [Tooltip("Since I can't figure out how to save position, just limit encounters so player can freely move through dungeon")]
    [SerializeField] int maxEncounters = 2;

    // Serialized for testing and visibility...
    [SerializeField] int numEncounters = 0;
    SwitchScene switchScene;

    void Awake()
    {
        switchScene = GetComponent<SwitchScene>();
        numEncounters = PlayerPrefs.GetInt("NUM_ENCOUNTERS", 0);
        Debug.Log("numEncounters" + numEncounters);
    }

    public void AttemptEncounter()
    {
        Debug.Log("Checking encounter...");
        if (numEncounters < maxEncounters && UnityEngine.Random.Range(1, chanceDenominator + 1) == chanceDenominator)
        {
            PlayerPrefs.SetInt("NUM_ENCOUNTERS", numEncounters + 1);

            Debug.Log("Entering battle!");
            switchScene.LoadNextScene();
        }
    }
}
