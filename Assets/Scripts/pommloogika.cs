using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pommloogika : MonoBehaviour
{
    // Start is called before the first frame update
    public float plahvatusAeg = 2.0f;
    void Start()
    {
        print("hakkab pauku tegema");
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion() {
        print("kohe teeb pauku");
        yield return new WaitForSeconds(plahvatusAeg);
        GameObject.Find("puusad").SendMessage("Explode", transform);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
