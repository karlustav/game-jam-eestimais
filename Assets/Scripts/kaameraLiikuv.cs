using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaameraLiikuv : MonoBehaviour
{
    public Transform player;        // Assign your player in the Inspector
    public float xCenter = 0f;      // Fixed X position of the camera
    public float zOffset = -10f;    // Default camera distance in Z
    public float smoothSpeed = 0.125f;

    // max ja min kaamera liikumine
    public float minY = 0f;
    public float maxY = 100f;


    void LateUpdate()
    {
        Vector3 desiredPosition = new Vector3(
            xCenter,
            player.position.y,
            zOffset
        );

        // Smooth movement (optional) vaatame mis saab lol
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
