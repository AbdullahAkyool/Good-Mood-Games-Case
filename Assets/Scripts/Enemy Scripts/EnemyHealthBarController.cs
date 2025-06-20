using System;
using UnityEngine;
using DG.Tweening;

public class EnemyHealthBarController : ProgressBarController
{
    public void UpdateHealthBar(float healthPercent)
    {
        fillBar.DOFillAmount(healthPercent, .5f);
        fillBar.DOColor(Color.Lerp(Color.red, Color.green, healthPercent), 1f);
    }

    public void CloseHealthBar()
    {
        fillBar.gameObject.SetActive(false);
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
