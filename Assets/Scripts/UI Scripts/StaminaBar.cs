using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public Gradient gradient;
    public Image fill;
    public ParticleSystem staminaPS;

    public static StaminaBar instance;
    private int currentStamina;
    private int maxStamina = 100;
    private WaitForSeconds refillTick = new WaitForSeconds(500f);
    private Coroutine refill;

    public bool canRun;
    public bool shiftPressing;

    private void Awake()
    {
        instance = this;
        staminaPS = GameObject.Find("FillStamina").GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
        canRun = true;
    }

    private void Update()
    {
        if(currentStamina == 100) staminaPS.Stop();
    }

    public void UsingStamina(int amount) 
    {
        if (currentStamina - amount >= 0)
        {
            staminaPS.Stop();
            currentStamina -= amount;
            staminaBar.value = currentStamina;
            
            if (refill != null) StopCoroutine(RefillStamina());
            refill = StartCoroutine(RefillStamina());
        }
    }

    public void CheckStamina() 
    {
        if (currentStamina == 0)
        {
            staminaPS.Stop();
            canRun = false;
        }
        else canRun = true;

    }

    private IEnumerator RefillStamina() 
    {
        yield return new WaitForSeconds(5f);
        staminaPS.Play();
        while (currentStamina < maxStamina) 
        {
            currentStamina += 1;
            staminaBar.value = currentStamina;
            yield return refillTick;            
        }
        
        refill = null;
    }
}
