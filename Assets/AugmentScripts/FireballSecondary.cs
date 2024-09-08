using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FireballSecondary", order = 1)]
public class FireballSecondary : SecondaryAttackAugmentBase
{
    public float damage;

    [SerializeField]
    public GameObject fireBallEffect;
    [SerializeField]
    public float radius;

    public override void StartSecondaryAttack(PlayerImpact info)
    {
        GameObject t = Instantiate(fireBallEffect, info.impactPos, fireBallEffect.transform.rotation);
        t.transform.localScale = Vector3.one * radius;
        Destroy(t, 3);

        if (info.hitObj.CompareTag("Enemie"))
        {
            info.hitObj.GetComponent<HealthScript>().TakeDamage(damage);
        }
    }
}
