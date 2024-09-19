using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentTable : MonoBehaviour
{

    public GameObject popUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.gameObject.CompareTag("Player"))
        {
            other.transform.parent.gameObject.GetComponent<PlayerAugmentController>().canChangeAugment = true;
            popUp.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent.gameObject.CompareTag("Player"))
        {
            other.transform.parent.gameObject.GetComponent<PlayerAugmentController>().canChangeAugment = false;
            popUp.SetActive(false);
        }
    }
}
