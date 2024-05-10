using UnityEngine;
using System.Collections;
using TMPro;

// Stolen from:
// https://discussions.unity.com/t/how-to-make-text-that-is-writen-automatically-letter-by-letter/15945/6
public class StoryTeller : MonoBehaviour
{


    [Header("Config")]
    [SerializeField] float letterPause = 0.2f;
    [SerializeField] TextMeshProUGUI textComponent;

    // public AudioClip typeSound1;
    // public AudioClip typeSound2;

    [Header("Text & Images")]
    [SerializeField] string[] textToType;


    string message;
    bool isTyping = false;
    int currentLineIndex = 0;
    Coroutine typingCoroutine;


    void Start()
    {
        ClearAndStartTyping();
    }

    void Update()
    {
        bool isLeftClickPressed = Input.GetMouseButtonDown(0);

        if (isLeftClickPressed)
        {
            // Move to next line
            if (!isTyping && currentLineIndex < textToType.Length - 1)
            {
                currentLineIndex += 1;
                ClearAndStartTyping();
            }
            // Stop typing and show entire line.
            else if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                textComponent.text = textToType[currentLineIndex];
                isTyping = false;
            }
            else
            {
                GetComponent<SwitchScene>().LoadNextScene();
            }
        }
    }

    void ClearAndStartTyping()
    {
        isTyping = true;
        textComponent.text = "";
        typingCoroutine = StartCoroutine(TypeText(currentLineIndex));
    }

    IEnumerator TypeText(int currentLineIndex)
    {
        message = textToType[currentLineIndex];

        foreach (char letter in message.ToCharArray())
        {
            textComponent.text += letter;
            // if (typeSound1 && typeSound2)
            // SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }

        isTyping = false;
    }
}