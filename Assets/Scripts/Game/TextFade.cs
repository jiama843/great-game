using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextFade : MonoBehaviour
{
    public Text textComponent;
    public float fadeDuration = 1.0f;
    public float fadeDelay = 1.0f;
    public float textDisplayTime = 1.0f;

    void Start()
    {
        Color textColor = textComponent.color;
        textColor.a = 0f;
        textComponent.color = textColor;

    }

    public void StartTextShow()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(fadeDelay);

        // Gradually increase the alpha value of the text color
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            Color textColor = textComponent.color;
            textColor.a = alpha;
            textComponent.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(textDisplayTime);

        // After fading in, start the fade-out effect
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeDelay);

        // Gradually decrease the alpha value of the text color
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            Color textColor = textComponent.color;
            textColor.a = alpha;
            textComponent.color = textColor;
            yield return null;
        }
    }
}
