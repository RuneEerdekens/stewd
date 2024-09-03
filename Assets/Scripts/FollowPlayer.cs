using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Vector3 offset;
    public Transform player;

    private void Start()
    {
        offset = transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
