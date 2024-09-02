using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
	public float moveSpeed;

	public float dashForce;
	public float dashTime;
	public float dashCooldown;

	public Material dashCharging;
	public Material dashCharged;

	private Rigidbody rb;
	private bool canDash = true;
	private float lastDashTime;
	private bool canMove = true;

	public GameObject MoveEffect;
	public GameObject DashIndicator;

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
		rb.velocity = Vector3.Lerp(rb.velocity, moveDir, 0.65f);
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
		MoveEffect.GetComponent<ParticleSystem>().enableEmission = rb.velocity.magnitude > 1;
	}

	private void DashEffect()
    {

		float scaleFactor = Mathf.Clamp01((Time.time - lastDashTime) / dashCooldown);
		DashIndicator.transform.localScale = Vector3.one * scaleFactor;
		DashIndicator.GetComponent<Renderer>().material = scaleFactor >= 1 ? dashCharged : dashCharging;
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
