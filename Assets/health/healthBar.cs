using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{

    [SerializeField]
    private Image foregroundImage;
    [SerializeField]
    private float ubdateSpeedSeconds = 0.5f;

    private void Awake()
    {
        GetComponentInParent<health>().OnHealthPctChanged += HandleHealthChanged;
    }


    private void HandleHealthChanged(float pct)
    {
       // foregroundImage.fillAmount = pct;
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct (float pct)
    {
        float preChangePct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while(elapsed < ubdateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / ubdateSpeedSeconds);
            yield return null;
        }

        foregroundImage.fillAmount = pct;
    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
