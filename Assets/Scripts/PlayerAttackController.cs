using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackController : MonoBehaviour
{
    public float primaryDamageAmount;
    public float secondaryDamageAmount;

    public float attackDowntime;

    public GameObject hitbox;
    public float pAttackCD;
    public float pAttackTime;

    public float sAttackCD;
    public float sAttackTime;

    public UnityEvent<Vector3> primaryEvent = new UnityEvent<Vector3>();
    public UnityEvent<Vector3> secondaryEvent = new UnityEvent<Vector3>();

    private bool canPAttack = true;
    private bool canSAttack = true;
    private bool isPAttacking = false;
    private bool isSAttacking = false;

    private bool canAttack = true;
    private bool isAttacking = false;

    void Update()
    {
        RotatePlayer();
        if (canPAttack && Input.GetMouseButtonDown(0) && !isAttacking && canAttack) //only primary attack when you can primary attack, hit mouse 0 and are not secondary attacking
        {
            PAttack();
        }

        if (canSAttack && Input.GetMouseButtonDown(1) && !isAttacking && canAttack)
        {
            SAttack();
        }
    }

    private void PAttack()
    {
        StartCoroutine(PAttackCd());
        StartCoroutine(PSwing());
        StartCoroutine(attackCd());
    }

    private void SAttack()
    {
        StartCoroutine(SAttackCd());
        StartCoroutine(SSwing());
        StartCoroutine(attackCd());
    }

    private IEnumerator attackCd()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackDowntime);
        canAttack = true;
    }

    private IEnumerator PAttackCd()
    {
        canPAttack = false;
        yield return new WaitForSeconds(pAttackCD);
        canPAttack = true;
    }

    private IEnumerator PSwing()
    {
        isAttacking = true;
        isPAttacking = true;
        hitbox.SetActive(true);
        yield return new WaitForSeconds(pAttackTime);
        isPAttacking = false;
        isAttacking = false;
        hitbox.SetActive(false);
    }

    private IEnumerator SAttackCd()
    {
        canSAttack = false;
        yield return new WaitForSeconds(sAttackCD);
        canSAttack = true;
    }

    private IEnumerator SSwing()
    {
        isAttacking = true;
        isSAttacking = true;
        hitbox.SetActive(true);
        yield return new WaitForSeconds(sAttackTime);
        isSAttacking = false;
        isAttacking = false;
        hitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemie"))
        {
            Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            if (isPAttacking)
            {
                primaryEvent.Invoke(contactPoint);
                other.GetComponent<HealthScript>().TakeDamage(primaryDamageAmount);
            }
            else if(isSAttacking)
            {
                secondaryEvent.Invoke(contactPoint);
                other.GetComponent<HealthScript>().TakeDamage(secondaryDamageAmount);
            }
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
