using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeUI : MonoBehaviour
{
    private CanvasGroup CanvasGroup;
    private GraphicRaycaster gr;

    [SerializeField] private float fadeTime = 0.5f;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
        gr = GetComponent<GraphicRaycaster>();
    }

    public void FadeIn(bool Instant)
    {
        gr.enabled = true;
        Fade(1f, Instant);
    }

    public void FadeOut(bool Instant)
    {
        gr.enabled = false;
        Fade(0f, Instant);
    }

    private void Fade(float targetAlpha, bool instant)
    {
        if (instant)
        {
            CanvasGroup.alpha = targetAlpha;
        }
        else
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeRoutine(targetAlpha));
        }
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = CanvasGroup.alpha;

        for(float timer = 0f; timer < fadeTime; timer += Time.deltaTime)
        {
            CanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeTime);
            yield return null;
        }
        CanvasGroup.alpha = targetAlpha;
        fadeCoroutine = null;
    }
}
