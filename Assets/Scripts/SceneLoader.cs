using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stolen from:
// https://blog.insane.engineer/post/unity_button_load_scene/
public class SceneLoader : MonoBehaviour
{
    [SerializeField] string sceneName;
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
