using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoadingScene : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<SwitchScene>().LoadNextScene();
    }
}
