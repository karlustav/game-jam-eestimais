using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float explosionRadius = 5.0f;
    public float explosionStrength = 10.0f;
    private Rigidbody2D rb2D;
    private BoxCollider2D box2D;
    private float maxSpeed = 20.0f;
    public float movesSpeed;
    

    void Start()
    {
        rb2D = gameObject.AddComponent<Rigidbody2D>();
        rb2D.mass = 0.4f;
        rb2D.gravityScale = 5;
        box2D = gameObject.AddComponent<BoxCollider2D>();
        rb2D.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) {
            print("liigun vasakule");
            rb2D.AddForce(-transform.right * movesSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.D)) {
            print("liigun paremale");
            rb2D.AddForce(transform.right * movesSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
        if (rb2D.velocity.magnitude > maxSpeed)
        {
            rb2D.velocity = rb2D.velocity.normalized * maxSpeed;
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
}
