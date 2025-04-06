using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mangijaController : MonoBehaviour
{
    public Animator anima;
    public Rigidbody2D rb;
    public float playerSpeed;



    // Start is called before the first frame update
    void Start()
    {
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
        if(Input.GetAxisRaw("Horizontal") != 0){
            if(Input.GetAxisRaw("Horizontal") > 0){
                anima.Play("walk");
                rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
            }
            else if(Input.GetAxisRaw("Horizontal") < 0){
                anima.Play("walkback");
                rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
            }
            else{
                anima.Play("idle");

            }
        }
        
    }
}
