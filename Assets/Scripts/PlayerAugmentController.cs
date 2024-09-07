using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerEvents))]
public class PlayerAugmentController : MonoBehaviour
{
    private PlayerEvents eventScript;
    public GameObject menuScreen;
    private bool isUiActive;

    private Dictionary<string, DashAugmentBase> dashAugments = new Dictionary<string, DashAugmentBase>();
    private Dictionary<string, PrimaryAttackAugmentBase> primaryAttackAugments = new Dictionary<string, PrimaryAttackAugmentBase>();
    private Dictionary<string, SecondaryAttackAugmentBase> secondaryAttackAugments = new Dictionary<string, SecondaryAttackAugmentBase>();

    //we store augemnts in dicts with the id as key and augment object itselve as value

    private void Awake()
    {
        eventScript = gameObject.GetComponent<PlayerEvents>();
    }

    private void Update()
    {
        isUiActive = Input.GetKey(KeyCode.E);
        menuScreen.SetActive(isUiActive);
        GetComponent<PlayerAttackController>().enabled = !isUiActive;
        Time.timeScale = isUiActive ? 0 : 1;
    }

    public void AssignAugmentToSlot(ScriptableObject augment)
    {
        if (augment is DashAugmentBase dashAugment)
        {
            dashAugments[dashAugment.ID] = dashAugment;
            eventScript.dashEvent.AddListener(dashAugment.StartDashEffect);
        }
        else if (augment is PrimaryAttackAugmentBase primaryAttackAugment)
        {
            primaryAttackAugments[primaryAttackAugment.ID] = primaryAttackAugment;
            eventScript.primaryAttackEvent.AddListener(primaryAttackAugment.StartPrimaryEffect);
        }
        else if (augment is SecondaryAttackAugmentBase secondaryAttackAugment)
        {
            secondaryAttackAugments[secondaryAttackAugment.ID] = secondaryAttackAugment;
            eventScript.secondaryAttackEvent.AddListener(secondaryAttackAugment.StartSecondaryAttack);
        }
    }

    public void RemoveAugmentFromSlot(ScriptableObject augment)
    {
        if (augment is DashAugmentBase dashAugment && dashAugments.ContainsKey(dashAugment.ID))
        {
            dashAugments.Remove(dashAugment.ID);
            eventScript.dashEvent.RemoveListener(dashAugment.StartDashEffect);
        }
        else if (augment is PrimaryAttackAugmentBase primaryAttackAugment && primaryAttackAugments.ContainsKey(primaryAttackAugment.ID))
        {
            primaryAttackAugments.Remove(primaryAttackAugment.ID);
            eventScript.primaryAttackEvent.RemoveListener(primaryAttackAugment.StartPrimaryEffect);
        }
        else if (augment is SecondaryAttackAugmentBase secondaryAttackAugment && secondaryAttackAugments.ContainsKey(secondaryAttackAugment.ID))
        {
            secondaryAttackAugments.Remove(secondaryAttackAugment.ID);
            eventScript.secondaryAttackEvent.RemoveListener(secondaryAttackAugment.StartSecondaryAttack);
        }
    }
}
