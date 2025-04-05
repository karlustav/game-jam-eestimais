using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollFixer : MonoBehaviour
{
    private Rigidbody2D[] allRigidbodies;
    private HingeJoint2D[] allJoints;
    private const float maxAllowedVelocity = 300f;

    void Start()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody2D>();
        allJoints = GetComponentsInChildren<HingeJoint2D>();
    }

    void Update()
    {
        foreach (Rigidbody2D rb in allRigidbodies)
        {
            // Fix NaN positions or rotations
            if (float.IsNaN(rb.position.x) || float.IsNaN(rb.rotation))
            {
                Debug.LogWarning("Fixing broken Rigidbody2D");
                rb.position = Vector2.zero;
                rb.rotation = 0f;
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            // Clamp extreme velocities
            if (rb.velocity.magnitude > maxAllowedVelocity)
            {
                rb.velocity = rb.velocity.normalized * maxAllowedVelocity;
            }
        }

        foreach (HingeJoint2D joint in allJoints)
        {
            if (joint != null && (joint.connectedBody == null || !joint.enabled))
            {
                Debug.LogWarning("Fixing or disabling broken joint");
                joint.enabled = false; // Or try to reconnect
            }
        }
    }
}
