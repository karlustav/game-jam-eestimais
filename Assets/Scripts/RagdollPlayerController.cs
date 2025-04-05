using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPlayerController : MonoBehaviour
{
    public Animator animator;
    public float playerSpeed = 10f;
    public float maxSpeed = 20f;
    public float airControlMultiplier = 0.4f; // ⬅️ New: controls how slow movement is in air
    public float explosionRadius = 5.0f;
    public float explosionStrength = 10.0f;

    public GameObject bombPrefab;
    public float bombPower = 5f;
    public float maxBombDistance = 10f;

    public Transform groundCheck; // ⬅️ New: assign in Inspector or create at runtime
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private LineRenderer lineRenderer;
    private Vector2 dragStartPos;
    private List<Rigidbody2D> ragdollRigidbodies = new List<Rigidbody2D>();

    void Start()
    {
        ragdollRigidbodies.AddRange(GetComponentsInChildren<Rigidbody2D>());

        Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i + 1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        Material m = new Material(Shader.Find("Transparent/Diffuse"));
        m.color = Color.white;
        lineRenderer.material = m;

        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.parent = transform;
            gc.transform.localPosition = new Vector3(0, -0.5f, 0); // Adjust to bottom of ragdoll
            groundCheck = gc.transform;
        }
    }

    void Update()
    {
        CheckGrounded();
        HandleMovement();
        HandleBombThrowing();
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            animator.Play(horizontal > 0 ? "walk" : "walkback");

            float currentSpeed = isGrounded ? playerSpeed : playerSpeed * airControlMultiplier;

            foreach (var rb in ragdollRigidbodies)
            {
                rb.AddForce(Vector2.right * horizontal * currentSpeed * Time.deltaTime, ForceMode2D.Impulse);

                if (rb.velocity.magnitude > maxSpeed)
                    rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
        else
        {
            animator.Play("idle");
        }
    }

    void HandleBombThrowing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = Vector2.ClampMagnitude((dragEndPos - dragStartPos) * bombPower, maxBombDistance);

            Vector2[] trajectory = Plot(ragdollRigidbodies[0], (Vector2)transform.position, velocity, 500);
            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
                positions[i] = trajectory[i];

            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = Vector2.ClampMagnitude((dragEndPos - dragStartPos) * bombPower, maxBombDistance);

            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bombRb = bomb.GetComponent<Rigidbody2D>();
            bombRb.gravityScale = 5f;
            bombRb.mass = 0.4f;
            bombRb.AddForce(velocity, ForceMode2D.Impulse);

            lineRenderer.positionCount = 0;
        }
    }

    public void Explode(Transform source)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            float distance = Vector2.Distance(rb.position, source.position);
            if (distance < explosionRadius)
            {
                float strength = explosionStrength * (1f - (distance / explosionRadius));
                rb.AddForce((rb.position - (Vector2)source.position).normalized * strength, ForceMode2D.Impulse);

                if (rb.velocity.magnitude > maxSpeed)
                    rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    Vector2[] Plot(Rigidbody2D body, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];
        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * body.gravityScale * timestep * timestep;
        float drag = 1f - timestep * body.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }
        return results;
    }
}
