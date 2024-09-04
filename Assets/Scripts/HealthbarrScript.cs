using UnityEngine.UI;
using UnityEngine;


[RequireComponent(typeof(HealthScript))]

public class HealthbarrScript : MonoBehaviour
{
    public Gradient HealthGradient;
    public Slider Healthbar;
    public bool rotate;

    private HealthScript healthScript;

    private void Start()
    {
        healthScript = gameObject.GetComponent<HealthScript>();

        healthScript.OnChangedMaxHealth += updateMaxHealth;

        Healthbar.maxValue = healthScript.maxHealth;
        Healthbar.value = healthScript.maxHealth;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(1);
    }

    void Update()
    {
        if (rotate)
        {
            Healthbar.transform.parent.transform.LookAt(Camera.main.transform.position);
        }
    }

    public void UpdateHealthbar()
    {
        float CurrHP = healthScript.getCurrentHealth();
        Healthbar.value = CurrHP;
        Healthbar.fillRect.gameObject.GetComponent<Image>().color = HealthGradient.Evaluate(CurrHP / healthScript.maxHealth);
    }

    private void updateMaxHealth(float newMax)
    {
        Healthbar.maxValue = newMax;
        UpdateHealthbar();
    }
}
