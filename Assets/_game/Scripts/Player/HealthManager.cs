using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public RawImage[] heartImages; // Kalp resimleri

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true; // Can kadar kalp resmini göster
            }
            else
            {
                heartImages[i].enabled = false; // Geri kalan kalpleri gizle
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        Debug.Log("Karakter Öldü!");
    }
}