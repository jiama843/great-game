using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class KeyButton : MonoBehaviour
{

    public KeyCode key;

    [SerializeField] Button button;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Down();
        }
    }
    // TODO: FIGURE OUT WHY THIS ISN"T WORKING
    void Down()
    {
        Debug.Log("Pressed" + key.ToString());
        button.onClick.Invoke();
    }


}