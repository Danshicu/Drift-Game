using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 Offset;
    public Vector3 Offset2;
    public Vector3 velocity;
    public float smoothTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(player.position + Offset2);
        Vector3 targetPosition = player.position + player.transform.TransformVector(Offset) + player.forward * (-5f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
