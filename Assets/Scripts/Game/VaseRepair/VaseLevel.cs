using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VaseLevel : MonoBehaviour
{
    public GameObject[] pieces;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameCheck()
    {
        int placedPieces = 0;
        foreach(GameObject p in pieces)
        {
            VaseSystem check = p.GetComponent<VaseSystem>();

            if (!check.isInteractable)
            {
                placedPieces++;
            }
        }

        if(placedPieces == pieces.Length)
        {
            //WIN
            Debug.Log("WIN");

            int jars = PlayerPrefs.GetInt("SLIME_JARS", 0);
            PlayerPrefs.SetInt("SLIME_JARS", jars--);

            //set game as complete
            GamesTracker.SetItemCompletionStatus(PlayerPrefs.GetString("itemID"), true);
            //return
            SceneManager.LoadScene("TileTestScene");
        }
        else {
            Debug.Log("Pieces are not in their places!");
        }
    }
}
