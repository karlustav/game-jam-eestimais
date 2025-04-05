using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float explosionRadius = 10.0f;
    public float explosionStrength = 100000.0f;
    public Rigidbody2D rb2D;
    private BoxCollider2D box2D;
    private float maxSpeed = 300.0f;
    public float movesSpeed = 1000.0f;
    public float airControlMultiplier = 0.4f; // 👈 Movement slower in air
    public Animator anima;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.mass = 0.4f;
        rb2D.gravityScale = 5;
        box2D = gameObject.AddComponent<BoxCollider2D>();
        rb2D.freezeRotation = true;

        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i + 1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }

        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.parent = transform;
            gc.transform.localPosition = new Vector3(0, -1f, 0); // Adjust if needed
            groundCheck = gc.transform;
        }
    }

    void Update()
    {
        CheckGrounded();

        float speed = isGrounded ? movesSpeed : movesSpeed * airControlMultiplier;

        if (Input.GetKey(KeyCode.A))
        {
            print("liigun vasakule");
            rb2D.AddForce(-transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            anima.Play("walk");
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("liigun paremale");
            rb2D.AddForce(transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
            anima.Play("walk");
        }

        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Explode(Transform t)
    {
        float distance = Vector2.Distance(gameObject.transform.position, t.position);
        distance *= distance;
        print(distance);
        if (distance < explosionRadius)
        {
            print("plahvatuse lahedal");
            float strength = explosionStrength * (distance - explosionRadius) / (-explosionRadius);
            rb2D.AddForce((gameObject.transform.position - t.position) * strength, ForceMode2D.Impulse);
        }
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
    }
}
