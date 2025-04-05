using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class ragdolljoinlimit : MonoBehaviour
{
    public float minAngle = -40f;
    public float maxAngle = 40f;

    void Start()
    {
        HingeJoint2D joint = GetComponent<HingeJoint2D>();
        JointAngleLimits2D limits = joint.limits;
        limits.min = minAngle;
        limits.max = maxAngle;
        joint.limits = limits;
        joint.useLimits = true;
    }
}