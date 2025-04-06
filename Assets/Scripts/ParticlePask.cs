using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePask : MonoBehaviour
{
    public ParticleSystem dustEffect;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.05f;
    public int dustLayer = 0; // Set to the correct layer (e.g., Default = 0)

    private bool wasGrounded;
    private bool dustCooldown;
    private Rigidbody2D rb;
    private Transform groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        groundCheck = new GameObject("GroundCheck").transform;
        groundCheck.SetParent(transform);
        groundCheck.localPosition = new Vector3(0, -0.1f, 0);
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (!wasGrounded && isGrounded && rb.velocity.y < -0.2f && !dustCooldown)
        {
            SpawnDust();
        }

        wasGrounded = isGrounded;
    }

    void SpawnDust()
    {
        if (dustEffect != null)
        {
            ParticleSystem fx = Instantiate(dustEffect, groundCheck.position, Quaternion.identity);
            fx.Play();

            // 🔥 FIX: Force it to the right sorting layer
            var renderer = fx.GetComponent<ParticleSystemRenderer>();
            renderer.sortingLayerName = "Characters";  // Or whatever your character layer is
            renderer.sortingOrder = 10;                // Make sure it's higher than background

            Destroy(fx.gameObject, fx.main.duration + fx.main.startLifetime.constantMax);
        }
    }

    IEnumerator DustCooldownRoutine()
    {
        dustCooldown = true;
        yield return new WaitForSeconds(0.1f); // prevents spamming on bouncy landings
        dustCooldown = false;
    }
}
