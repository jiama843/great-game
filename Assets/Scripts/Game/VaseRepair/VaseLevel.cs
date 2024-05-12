using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VaseLevel : MonoBehaviour
{
    public GameObject[] pieces;
    // Start is called before the first frame update
    void Start()
    {
       
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
        }
        else {
            Debug.Log("Pieces are not in their places!");
        }
    }
}
