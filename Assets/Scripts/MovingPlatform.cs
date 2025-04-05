using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;

    private Vector3 target;

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Assign both pointA and pointB in the inspector.");
            enabled = false;
            return;
        }

        target = pointB.position;
    }

    private void Update()
    {
        // Move platform towards the current target
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the platform reached the target, then switch
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
}
