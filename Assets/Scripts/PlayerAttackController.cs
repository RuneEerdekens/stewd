using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float damageAmount;

    public GameObject hitbox;
    public float attackCD;
    public float attackTime;

    private bool canAttack = true;

    void Update()
    {
        RotatePlayer();
        if (canAttack && Input.GetMouseButton(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        StartCoroutine(AttackCd());
        StartCoroutine(Swing());
    }

    private IEnumerator AttackCd()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCD);
        canAttack = true;
    }

    private IEnumerator Swing()
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        hitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemie"))
        {
            other.GetComponent<HealthScript>().TakeDamage(damageAmount);
        }
    }

    private void RotatePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(ray, out rayLength))
        {
            Vector3 pointToLook = ray.GetPoint(rayLength);


            Vector3 direction = (pointToLook - transform.position).normalized;
            direction.y = 0;


            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
