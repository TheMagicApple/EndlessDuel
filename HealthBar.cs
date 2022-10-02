using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{


    public Image healthBar;
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        float ratio = currentHealth / maxHealth;
        healthBar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        if (ratio > 0.5f)
        {
            healthBar.color = Color.green;
        }
        else if (ratio > 0.2f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.red;
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }



}