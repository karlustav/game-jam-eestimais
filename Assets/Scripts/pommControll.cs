using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pommControll : MonoBehaviour{
    // paar muutujat
    public float power = 5f;
    public float maxDistance = 50f;
    Rigidbody2D rb;
    LineRenderer lr;
    Vector2 DragStartPos;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

    }

    
    private void Update(){
        if(Input.GetMouseButtonDown(0)){// 0 - vasak hiireklahv alla
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // võtab alguse pos hiire kliki asukoha

        }

        if (Input.GetMouseButton(0)){ // draggimise ajal
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = Vector2.ClampMagnitude((DragEndPos - DragStartPos) * power, maxDistance);

            Vector2[] trajektoor = Plot(rb, (Vector2)transform.position, _velocity, 500);

            lr.positionCount = trajektoor.Length;

            Vector3[] positions = new Vector3[trajektoor.Length];
            for (int i = 0; i < trajektoor.Length; i++){
                positions[i] = trajektoor[i];
            }

            lr.SetPositions(positions);

        }

        if (Input.GetMouseButtonUp(0)){ // hiireklahv üles
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = Vector2.ClampMagnitude((DragEndPos - DragStartPos) * power;

            rb.velocity = _velocity;

            lr.positionCount = 0;

        }
    
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps){ // pmst mõtleb välja kus kohas kõik joone punktid peaksid asuma
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep*rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; i++){
            moveStep += gravityAccel;
            moveStep *= drag;
            pos+= moveStep;
            results[i] = pos;
        }
        return results;

    }
}
 