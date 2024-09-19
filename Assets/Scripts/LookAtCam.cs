
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(Camera.main.transform.position);
    }
}
