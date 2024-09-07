using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerCharacterController))]
[RequireComponent(typeof(PlayerAttackController))]
public class PlayerEvents : MonoBehaviour
{

    public UnityEvent<Vector3> dashEvent = new UnityEvent<Vector3>();
    public UnityEvent<PlayerImpact> primaryAttackEvent = new UnityEvent<PlayerImpact>();
    public UnityEvent<PlayerImpact> secondaryAttackEvent = new UnityEvent<PlayerImpact>();


    private void Start()
    {
        GetComponent<PlayerCharacterController>().onDash.AddListener(OnDash);

        PlayerAttackController attackController = GetComponent<PlayerAttackController>();
        attackController.primaryEvent.AddListener(OnPrimaryAttack);
        attackController.secondaryEvent.AddListener(OnSecondaryAttack);
    }

    public void OnDash(Vector3 pos)
    {
        dashEvent.Invoke(pos);
        print("Dashing..");
    }

    public void OnPrimaryAttack(PlayerImpact info)
    {
        primaryAttackEvent.Invoke(info);
        print("Primary attacking..");
    }

    public void OnSecondaryAttack(PlayerImpact info)
    {
        secondaryAttackEvent.Invoke(info);
        print("Secondary attacking..");
    }

}
