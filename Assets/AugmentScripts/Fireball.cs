using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Fireball", order = 1)]
public class Fireball : PrimaryAttackAugmentBase
{
    public float damage;

    [SerializeField]
    public GameObject fireBallEffect;
    [SerializeField]
    public float radius;

    public override void StartPrimaryEffect(PlayerImpact info)
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
