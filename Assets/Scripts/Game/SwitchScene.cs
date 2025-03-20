using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    [SerializeField] string sceneToLoad = "Scenes/CleaningScene";

    // Update is called once per frame
    void Update()
    {
        // Scuffed check for collision since it isn't working
        //CheckExitCondition();
    }

    void CheckExitCondition()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);

        if (hitColliders.Any(collider => collider.tag == "Player"))
        {
            LoadNextScene();
        }
    }

    // Made public so buttons etc can access and use it
    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    // Made public so buttons etc can access and use it
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
