using System.Collections;
using UnityEngine;

public class EffectsController : MonoBehaviour
{

    public AnimationCurve shakeCurve;
    public float shakeDuration;
    public GameObject dashStartEffect;
    public Material dashMat;


    public void DashStart()
    {
        Vector3 floorPosition = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) ? hit.point : transform.position;
        Destroy(Instantiate(dashStartEffect, floorPosition, dashStartEffect.transform.rotation), dashStartEffect.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        StartCoroutine(ScreenShakeRoutine());
    }

    private IEnumerator ScreenShakeRoutine()
    {
        float elapsedTime = 0f;

        while(elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strenght = shakeCurve.Evaluate(elapsedTime / shakeDuration);
            Camera.main.transform.localPosition = Random.insideUnitSphere * strenght;
            yield return null;
        }

        Camera.main.transform.localPosition = Vector3.zero;
    }
}
