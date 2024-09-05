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

    public override void StartPrimaryEffect(Vector3 impactPos)
    {
        GameObject t = Instantiate(fireBallEffect, impactPos, Quaternion.identity);
        t.transform.localScale = Vector3.one * radius;
        Destroy(t, 3);
        Collider[] cols = Physics.OverlapSphere(impactPos, radius);

        foreach (Collider col in cols)
        {
            if (col.CompareTag("Enemie"))
            {
                col.gameObject.GetComponent<HealthScript>().TakeDamage(damage);
            }
        }
    }
}
