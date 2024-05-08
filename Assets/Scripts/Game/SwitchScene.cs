using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        // Scuffed check for collision since it isn't working
        checkExitCondition();
    }

    void checkExitCondition(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

        if (hitColliders.Any(collider => collider.tag == "Player"))
        {
            SceneManager.LoadScene("Scenes/CleaningScene");
        }
    }

    // void onCollisionEnter(Collision collision){
    //     Debug.Log("Scene Switch collision");
    //     // if (other.tag == "BattleEnter")
    //     // {
    //     //     SceneManager.LoadScene("Scenes/CleaningScene");
    //     // }
    // }

    // private void onTriggerEnter(Collider other){
    //     Debug.Log("Scene Switch Trigger");
    //     // if (other.tag == "BattleEnter")
    //     // {
    //     //     SceneManager.LoadScene("Scenes/CleaningScene");
    //     // }
    // }
}
