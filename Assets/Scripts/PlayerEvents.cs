using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{

    public UnityEvent<Vector3> dashEvent = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> primaryAttackEvent = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> secondaryAttackEvent = new UnityEvent<Vector3>();

    public void OnDash(Vector3 pos)
    {
        dashEvent.Invoke(pos);
        print("Dashing..");
    }

    public void OnPrimaryAttack(Vector3 impactPos)
    {
        primaryAttackEvent.Invoke(impactPos);
        print("Primary attacking..");
    }

    public void OnSecondaryAttack(Vector3 impactPos)
    {
        secondaryAttackEvent.Invoke(impactPos);
        print("Secondary attacking..");
    }

}
