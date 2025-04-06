using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionRadius = 5.0f;
    public float explosionStrength = 180.0f;
    public Rigidbody2D rb2D;
    private BoxCollider2D box2D;
    public float maxSpeed = 300.0f;
    public float movesSpeed = 60.0f;
    public Animator anima;

    public ParticleSystem dustEffect;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private bool wasGroundedLastFrame;


    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.mass = 0.4f;
        rb2D.gravityScale = 5;
        box2D = gameObject.AddComponent<BoxCollider2D>();
        rb2D.freezeRotation = true;

        
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++){
            for (int k = i+1; k < colliders.Length; k++){
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            print("liigun vasakule");
            rb2D.AddForce(-transform.right * movesSpeed * Time.deltaTime, ForceMode2D.Impulse);
            anima.Play("walk");

        }
        if (Input.GetKey(KeyCode.D)) {
            print("liigun paremale");
            rb2D.AddForce(transform.right * movesSpeed * Time.deltaTime, ForceMode2D.Impulse);
            anima.Play("walk");
        }
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }

        wasGroundedLastFrame = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!wasGroundedLastFrame && isGrounded)
        {
            SpawnDust();
        }
    }

    void Explode(Transform t) {
        float distance = Vector2.Distance(gameObject.transform.position, t.position);
        distance *= distance;
        print(distance);
        if (distance < explosionRadius) {
            print("plahvatuse lahedal");
            float strength = explosionStrength*(distance - explosionRadius)/(-explosionRadius);
            rb2D.AddForce((gameObject.transform.position - t.position)*strength, ForceMode2D.Impulse);
        }
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
        }
    }

    void SpawnDust()
    {
        if (dustEffect != null)
        {
            ParticleSystem fx = Instantiate(dustEffect, groundCheck.position, Quaternion.identity);
            fx.Play();
            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
        }
    }
}
