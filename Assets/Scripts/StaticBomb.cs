using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBomb : MonoBehaviour
{
    public float plahvatusAeg = 5.0f;

    IEnumerator ExplosionLoop() {
        while (true) {
            print("kohe teeb pauku");
            yield return new WaitForSeconds(plahvatusAeg);
            GameObject.Find("mangija").SendMessage("Explode", transform);
        }
    }

    void Start()
    {
        print("hakkab pauku tegema");
        StartCoroutine(ExplosionLoop());
    }
}