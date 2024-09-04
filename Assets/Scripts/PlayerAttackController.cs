using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackController : MonoBehaviour
{
    public float primaryDamageAmount;
    public float secondaryDamageAmount;

    public GameObject hitbox;
    public float pAttackCD;
    public float pAttackTime;

    public float sAttackCD;
    public float sAttackTime;

    public UnityEvent<Vector3, GameObject> primaryEvent = new UnityEvent<Vector3, GameObject>();
    public UnityEvent<Vector3, GameObject> secondaryEvent = new UnityEvent<Vector3, GameObject>();

    private bool canPAttack = true;
    private bool canSAttack = true;
    private bool isAttacking = false;

    void Update()
    {
        RotatePlayer();
        if (canPAttack && Input.GetMouseButtonDown(0) && !isAttacking) //only primary attack when you can primary attack, hit mouse 0 and are not secondary attacking
        {
            PAttack();
        }

        if (canSAttack && Input.GetMouseButtonDown(1) && !isAttacking)
        {
            SAttack();
        }
    }

    private void PAttack()
    {
        StartCoroutine(PAttackCd());
        StartCoroutine(PSwing());
    }

    private void SAttack()
    {
        StartCoroutine(SAttackCd());
        StartCoroutine(SSwing());
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
        hitbox.SetActive(true);
        yield return new WaitForSeconds(pAttackTime);
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
        hitbox.SetActive(true);
        yield return new WaitForSeconds(sAttackTime);
        isAttacking = false;
        hitbox.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemie"))
        {
            Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            if (!canPAttack)
            {
                primaryEvent.Invoke(contactPoint, other.gameObject);
                other.GetComponent<HealthScript>().TakeDamage(primaryDamageAmount);
            }
            else if(!canSAttack)
            {
                secondaryEvent.Invoke(contactPoint, other.gameObject);
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
