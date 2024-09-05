using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DashAugmentBase : ScriptableObject
{
    public abstract void StartDashEffect(Vector3 pos);
}

public abstract class PrimaryAttackAugmentBase: ScriptableObject
{
    public abstract void StartPrimaryEffect(Vector3 impactPos);
}

public abstract class SecondaryAttackAugmentBase : ScriptableObject
{
    public abstract void StartSecondaryAttack(Vector3 impactPos);
}
