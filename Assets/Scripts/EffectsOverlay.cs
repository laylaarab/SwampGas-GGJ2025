using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EffectsOverlay : MonoBehaviour
{
    private Image _overlay;

    private void OnEnable()
    {
        _overlay = GetComponent<Image>();
    }

    private IEnumerator LerpAlphaToTarget(float targetAlpha, float duration)
    {
        float startAlpha = _overlay.color.a;
        float rate = 1.0f / duration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            Color tempColor = _overlay.color;
            _overlay.color = new Color(tempColor.r, tempColor.g, tempColor.b, Mathf.Lerp(startAlpha, targetAlpha, progress));
            progress += rate * Time.deltaTime;

            yield return null;
        }

        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, targetAlpha);
    }

    public IEnumerator FadeIn(float duration)
    {
        yield return LerpAlphaToTarget(1, duration);
    }

    public IEnumerator FadeOut(float duration)
    {
        yield return LerpAlphaToTarget(0, duration);
    }

}
