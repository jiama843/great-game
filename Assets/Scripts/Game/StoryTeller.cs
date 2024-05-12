using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

[System.Serializable]
public class StorySlide {
    public Sprite bg;
    public List<string> textToType = new List<string>();
}

// Stolen from:
// https://discussions.unity.com/t/how-to-make-text-that-is-writen-automatically-letter-by-letter/15945/6
public class StoryTeller : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] float letterPause = 0.2f;
    [SerializeField] TextMeshProUGUI textComponent;
    public Image slideDisplay;

    public AudioSource flipSound;
    // public AudioClip flipSound;
    // public AudioClip typeSound2;

    [Header("Story Slides")]
    public List<StorySlide> storyslides = new List<StorySlide>();

    string message;
    bool isTyping = false;
    Coroutine typingCoroutine;

    private StorySlide currentStorySlide;
    private string currentTextToType;

    void Start()
    {
        // We're assuming all this shit exists
        currentStorySlide = storyslides.First();
        flipSound.Play();
        slideDisplay.sprite = currentStorySlide.bg;

        // TODO: Remove this and uncomment the code under it if we transition away from image only
        storyslides.RemoveAt(0);

        // currentTextToType = currentStorySlide.textToType.First();
        // ClearAndStartTyping();
        // currentStorySlide.textToType.RemoveAt(0);
    }

    void Update()
    {
        bool isLeftClickPressed = Input.GetMouseButtonDown(0);

        if (isLeftClickPressed)
        {
            if (storyslides.Count == 0) GetComponent<SwitchScene>().LoadNextScene();

            currentStorySlide = storyslides.First();

            if (currentStorySlide.textToType.Count == 0)
            {
                switchStorySlide();
                return;
            }

            currentTextToType = currentStorySlide.textToType.First();
            handleTypeText();
        }
    }

    void switchStorySlide(){
        storyslides.RemoveAt(0);
        
        //switch current image sprite
        flipSound.Play();
        slideDisplay.sprite = currentStorySlide.bg;
    }

    void handleTypeText(){
        if (!isTyping)
        {
            ClearAndStartTyping();
            currentStorySlide.textToType.RemoveAt(0);
        }
        else
        {
            StopCoroutine(typingCoroutine);
            isTyping = false;
        }
    }

    void ClearAndStartTyping()
    {
        isTyping = true;
        textComponent.text = "";
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        message = currentTextToType;

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