using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pommloogika : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("hakkab pauku tegema");
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion() {
        print("kohe teeb pauku");
        yield return new WaitForSeconds(3.0f);
        GameObject.Find("mangija").SendMessage("Explode", transform);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
