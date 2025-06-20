using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class ProgressBarController : MonoBehaviour
{
    public Image fillBar;
    private Tweener fillTween;
    private Sequence pulseSequence;
    public bool IsBarFilling => fillTween != null && fillTween.IsActive();

    public void ShowProgressBar(float duration, Action onComplete = null)
    {
        gameObject.SetActive(true);
        fillBar.fillAmount = 0f;
        fillBar.color = Color.green;

        fillTween?.Kill();
        pulseSequence?.Kill();

        // Nabız gibi büyüyüp küçülme efekti (sürekli tekrar eden)
        transform.localScale = Vector3.one;

        pulseSequence = DOTween.Sequence();
        pulseSequence.Append(transform.DOScale(1.15f, 0.15f).SetEase(Ease.OutSine));
        pulseSequence.Append(transform.DOScale(1f, 0.15f).SetEase(Ease.InSine));
        pulseSequence.SetLoops(-1);

        fillTween?.Kill();
        fillTween = fillBar.DOFillAmount(1f, duration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                fillBar.color = Color.Lerp(Color.green, Color.red, fillBar.fillAmount);
            })
            .OnComplete(() =>
            {
                onComplete?.Invoke();
                HideProgressBar();
            });
    }

    public void HideProgressBar()
    {
        fillTween?.Kill();
        pulseSequence?.Kill();
        transform.localScale = Vector3.one;

        gameObject.SetActive(false);
        fillBar.fillAmount = 0f;
    }

}
