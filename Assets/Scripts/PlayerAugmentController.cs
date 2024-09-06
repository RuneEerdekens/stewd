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

    private List<DashAugmentBase> dashAugments = new List<DashAugmentBase>();
    private List<PrimaryAttackAugmentBase> primaryAttackAugments = new List<PrimaryAttackAugmentBase>();
    private List<SecondaryAttackAugmentBase> secondaryAttackAugments = new List<SecondaryAttackAugmentBase>();



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
            dashAugments.Add(dashAugment);
            eventScript.dashEvent.AddListener(dashAugment.StartDashEffect);
        }
        else if (augment is PrimaryAttackAugmentBase primaryAttackAugment)
        {
            primaryAttackAugments.Add(primaryAttackAugment);
            eventScript.primaryAttackEvent.AddListener(primaryAttackAugment.StartPrimaryEffect);
        }
        else if (augment is SecondaryAttackAugmentBase secondaryAttackAugment)
        {
            secondaryAttackAugments.Add(secondaryAttackAugment);
            eventScript.secondaryAttackEvent.AddListener(secondaryAttackAugment.StartSecondaryAttack);
        }
    }

    public void RemoveAugmentFromSlot(ScriptableObject augment)
    {
        if (augment is DashAugmentBase dashAugment)
        {
            dashAugments.Remove(dashAugment);
            eventScript.dashEvent.RemoveListener(dashAugment.StartDashEffect);
        }
        else if (augment is PrimaryAttackAugmentBase primaryAttackAugment)
        {
            primaryAttackAugments.Remove(primaryAttackAugment);
            eventScript.primaryAttackEvent.RemoveListener(primaryAttackAugment.StartPrimaryEffect);
        }
        else if (augment is SecondaryAttackAugmentBase secondaryAttackAugment)
        {
            secondaryAttackAugments.Remove(secondaryAttackAugment);
            eventScript.secondaryAttackEvent.RemoveListener(secondaryAttackAugment.StartSecondaryAttack);
        }
    }
}
