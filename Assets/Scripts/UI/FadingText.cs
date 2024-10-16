using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadingText : MonoBehaviour
{
    [SerializeField] private float fadeOutAlpha = 0.25f; // New variable to control fade out alpha
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float fadeDelay = 0.2f;
    [SerializeField] private TextMeshProUGUI textComponent;
    private bool isFadingIn;
    private bool isFadingOut;


    private void Start()
    {
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(fadeDelay);
            yield return StartCoroutine(FadeOut());
            yield return new WaitForSeconds(fadeDelay);
        }
    }

    private IEnumerator FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;

        float elapsedTime = 0f;
        Color startColor = textComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            textComponent.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        isFadingIn = false;
    }

    private IEnumerator FadeOut()
    {
        isFadingIn = false;
        isFadingOut = true;

        float elapsedTime = 0f;
        Color startColor = textComponent.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, fadeOutAlpha); // Use fadeOutAlpha for fade out alpha value

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            textComponent.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        isFadingOut = false;
    }
}
