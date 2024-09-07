using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AugmentBase : ScriptableObject
{
    [SerializeField] private string id;

    public string ID => id;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = System.Guid.NewGuid().ToString();
        }
    }
}

public abstract class DashAugmentBase : AugmentBase
{
    public abstract void StartDashEffect(Vector3 pos);
}

public abstract class PrimaryAttackAugmentBase : AugmentBase
{
    public abstract void StartPrimaryEffect(PlayerImpact info);
}

public abstract class SecondaryAttackAugmentBase : AugmentBase
{
    public abstract void StartSecondaryAttack(PlayerImpact info);
}


// slots

public abstract class ISlot : MonoBehaviour, IDropHandler
{
    public abstract void OnDrop(PointerEventData eventData);
    public abstract void ApplyAugment(Augment aug);
}

public struct PlayerImpact
{
    public Vector3 impactPos { get; }
    public GameObject hitObj { get; }

    public PlayerImpact(Vector3 impactPos, GameObject hitObj)
    {
        this.impactPos = impactPos;
        this.hitObj = hitObj;
    }
}
