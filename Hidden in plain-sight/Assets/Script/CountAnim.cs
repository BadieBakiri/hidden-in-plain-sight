using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPanelAnimator : MonoBehaviour
{
    public RectTransform panel;
    public Image panelImage;
    public Color startColor = Color.white;
    public Color endColor = Color.red;
    public Vector2 minSize = new Vector2(100, 100);
    public Vector2 maxSize = new Vector2(200, 200);
    public float duration = 1f;

    public void AnimatePanel()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float sineValue = Mathf.Sin(t * Mathf.PI);

            panel.sizeDelta = Vector2.Lerp(minSize, maxSize, sineValue);
            panelImage.color = Color.Lerp(startColor, endColor, sineValue);

            yield return null;
        }

        panel.sizeDelta = minSize;
        panelImage.color = startColor;
    }
}
