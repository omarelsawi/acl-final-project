using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 200;

    private float currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };

 
    private void Awake()
    {
        GetComponentInParent<Boss>().CurrentHealth += HandleHealthChanged;
        Debug.Log("in Awake HandleHealthChanged");
    }


    private void HandleHealthChanged(float pct)
    {
        currentHealth = pct;
        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        OnHealthPctChanged(currentHealthPct);

      
    }

   

    // Update is called once per frame
    
}
