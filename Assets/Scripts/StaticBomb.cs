using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBomb : MonoBehaviour
{
    public float plahvatusAeg = 5.0f;
    IEnumerator Explosion() {
        print("kohe teeb pauku");
        yield return new WaitForSeconds(plahvatusAeg);
        GameObject.Find("mangija").SendMessage("Explode", transform);
    }

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        print("hakkab pauku tegema");
        StartCoroutine(Explosion());
    }
}
