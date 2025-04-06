using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pommloogika : MonoBehaviour
{
    // Start is called before the first frame update
    public float plahvatusAeg = 2.0f;
    public float animatsiooniKiirus = 0.2f;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        AudioClip pauk = Resources.Load<AudioClip>("pomm/bomba");
        var sprite1 = Resources.Load<Sprite>("pomm/pomm2_2");
        if (sprite1 == null) print("error");
        var sprite2 = Resources.Load<Sprite>("pomm/pomm3_3");
        var sprite3 = Resources.Load<Sprite>("pomm/pomm4_4");
        print("hakkab pauku tegema");
        audioSource.PlayOneShot(pauk);
        StartCoroutine(Explosion(sprite1, sprite2, sprite3, pauk.length));
    }
    IEnumerator Explosion(Sprite s1, Sprite s2, Sprite s3, float soundDuration) {
        print("kohe teeb pauku");
        yield return new WaitForSeconds(plahvatusAeg);
        GameObject.Find("puusad").SendMessage("Explode", transform);
        StartCoroutine(Frame1(s1, s2, s3, soundDuration));
    }

    IEnumerator Frame1(Sprite s1, Sprite s2, Sprite s3, float soundDuration) {
        print("1");
        gameObject.GetComponent<SpriteRenderer>().sprite = s1;
        yield return new WaitForSeconds(animatsiooniKiirus);

        print("2");
        gameObject.GetComponent<SpriteRenderer>().sprite = s2;
        yield return new WaitForSeconds(animatsiooniKiirus);

        print("3");
        gameObject.GetComponent<SpriteRenderer>().sprite = s3;
        yield return new WaitForSeconds(2 * animatsiooniKiirus);

        // ❌ Instead of destroying right away, hide the sprite visually
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // 🕒 Wait for the sound to finish before destruction
        yield return new WaitForSeconds(soundDuration);

        print("kill");
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
