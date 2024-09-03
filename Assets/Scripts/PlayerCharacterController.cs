using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacterController : MonoBehaviour
{
	public float moveSpeed;
	[Range(0, 1)]
	public float ResponsivenessSlider;

	public float dashForce;
	public float dashTime;
	public float dashCooldown;

	public Material dashCharging;
	public Material dashCharged;

	private Rigidbody rb;
	private bool canDash = true;
	private float lastDashTime;
	private bool canMove = true;

	public GameObject moveEffect;
	public GameObject dashIndicator;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
		lastDashTime = -dashCooldown;
	}

	void FixedUpdate()
	{
        if (canMove)
        {
			Move();
		}
        if (Input.GetKey(KeyCode.Space) && canDash && rb.velocity.magnitude > 1)
        {
			Dash();
        }

		MoveEffects();
		DashEffect();
	}


	private void Move()
    {
		float x = Input.GetAxisRaw("Horizontal");
		float z = Input.GetAxisRaw("Vertical");


		Vector3 moveDir = new Vector3(x, 0, z).normalized * Time.deltaTime * moveSpeed;
		rb.velocity = Vector3.Lerp(rb.velocity, moveDir, ResponsivenessSlider);
	}

	private void Dash()
    {
		StartCoroutine(StartDashCD());
		StartCoroutine(StartDashLockout());
		lastDashTime = Time.time;
		rb.AddForce(rb.velocity.normalized * dashForce, ForceMode.Impulse);
    }

	private void MoveEffects()
    {
		moveEffect.GetComponent<ParticleSystem>().enableEmission = rb.velocity.magnitude > 1;
	}

	private void DashEffect()
    {

		float scaleFactor = Mathf.Clamp01((Time.time - lastDashTime) / dashCooldown);
		dashIndicator.transform.localScale = Vector3.one * scaleFactor * 0.4f;
		dashIndicator.GetComponent<Renderer>().material = scaleFactor >= 1 ? dashCharged : dashCharging;
	}

	private IEnumerator StartDashCD()
    {
		canDash = false;
		yield return new WaitForSeconds(dashCooldown);
		canDash = true;
    }

	private IEnumerator StartDashLockout()
	{
		canMove = false;
		yield return new WaitForSeconds(dashTime);
		canMove = true;
	}
}
