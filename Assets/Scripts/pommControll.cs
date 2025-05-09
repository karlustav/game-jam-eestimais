 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pommControll : MonoBehaviour{
    // paar muutujat
    public GameObject pommPrefab;
    public float power = 5f;
    public float maxDistance = 10f;
    LineRenderer lr;
    Vector2 DragStartPos;
    Rigidbody2D rb;

    private void Start() {
        lr = GetComponent<LineRenderer>();
        //Material m = new Material(Shader.Find("Transparent/Diffuse"));
        //m.color = Color.white;
        //lr.material = m;
        lr.sortingOrder = 5;
        rb = gameObject.AddComponent<Rigidbody2D>();
    }

    
    private void Update(){
        
        if (Time.timeScale != 0){

    

            transform.localPosition = new Vector2(0, 0);
            if(Input.GetMouseButtonDown(0) && !IsPointerOverUI()){// 0 - vasak hiireklahv alla
                DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // võtab alguse pos hiire kliki asukoha

            }

            if (Input.GetMouseButton(0) && !IsPointerOverUI()){ // draggimise ajal
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 _velocity = Vector2.ClampMagnitude((DragEndPos - DragStartPos) * power, maxDistance);

                Vector2[] trajektoor = Plot(rb, (Vector2)transform.position, _velocity, 400);

                lr.positionCount = trajektoor.Length;

                Vector3[] positions = new Vector3[trajektoor.Length];
                for (int i = 0; i < positions.Length; i++){
                    positions[i] = trajektoor[i];
                }

                lr.SetPositions(positions);

            }

            if (Input.GetMouseButtonUp(0) && !IsPointerOverUI()){ // hiireklahv üles
                Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 _velocity = Vector2.ClampMagnitude((DragEndPos - DragStartPos) * power, maxDistance);
                GameObject pomm = Instantiate(pommPrefab, transform.position, pommPrefab.transform.rotation);
                pomm.layer = 9;
                pomm.GetComponent<SpriteRenderer>().sortingOrder = 1;
                Rigidbody2D pommRb = pomm.GetComponent<Rigidbody2D>();
                pommRb.gravityScale = 5f;
                pommRb.mass = 0.4f;
                pommRb.AddForce(_velocity, ForceMode2D.Impulse);

                lr.positionCount = 0;

            }
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

    private bool IsPointerOverUI() {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
}
}
 