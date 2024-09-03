using UnityEngine.Events;
using UnityEngine;
using System.Collections;
using System;

public class HealthScript : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;
    [SerializeField]
    private GameObject deathEffect;

    public UnityEvent<float> OnTakeDamage = new UnityEvent<float>();
    public UnityEvent onDie = new UnityEvent();

    public event Action<float> OnChangedMaxHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("DeathAwaits", 2, 2);
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"Hit: {gameObject.name}, Health: {currentHealth}.");

        if (currentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} died.");
            onDie.Invoke();
        }

        OnTakeDamage.Invoke(amount);
    }

    public void UpdateMaxHealth(float newMax)
    {
        maxHealth = newMax;
        currentHealth = maxHealth <= currentHealth ? maxHealth : currentHealth;

        OnChangedMaxHealth?.Invoke(maxHealth);
    }

    public void respawn()
    {
        currentHealth = maxHealth;
    }

    public void permaDie()
    {
        Destroy(gameObject, 0.01f);
    }

    public void spawnDeathEffect()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
    }

    private void DeathAwaits()
    {
        TakeDamage(10);
    }
}
