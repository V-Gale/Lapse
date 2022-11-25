using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    public Renderer player;
    public Material damaged;
    public Material baseColor;

    public void Awake()
    {
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        
    }

    public void SubstractLife(int amount) 
    {
        currentHealth -= amount;
        FindObjectOfType<AudioManager>().Play("PlayerDamaged");
        healthBar.SetHealth(currentHealth);
        player.material = damaged;
        StartCoroutine(ColorChange());

        if (currentHealth <= 0)
        {
            this.GetComponent<PlayerMovement>().enabled = false;
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            FindObjectOfType<GameManager>().GameOver();            
        }
    }

    public void AddLife(int amountRecover)
    {
        FindObjectOfType<AudioManager>().Play("Health");
        currentHealth += amountRecover;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    IEnumerator ColorChange()
    {
        player = this.GetComponent<Renderer>();
        player.material = damaged;
        yield return new WaitForSeconds(0.01f);
        player.material = baseColor;
    }

}
