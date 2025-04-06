using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Only needed if you're using Unity UI Text

public class RepeatingTimeBomb : MonoBehaviour
{
    [Header("Bomb Settings")]
    // Defaults based on your original script:
    public float countdownTime = 2.0f;
    public float explosionAnimationSpeed = 0.2f;

    [Header("Explosion Assets")]
    // These will be loaded from Resources if not assigned in the Inspector
    public AudioClip explosionSound;
    public Sprite[] explosionSprites;

    [Header("UI Display")]
    // Optional: Assign a UI Text element to show the countdown
    public Text countdownDisplay;             

    private AudioSource audioSource;
    private float timer;
    private bool isExploding = false;
    private Sprite originalSprite;

    void Start()
    {
        timer = countdownTime;
        
        // Add or get AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Disable gravity if Rigidbody2D is present
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 0;

        // Cache original sprite for later restoration
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalSprite = sr.sprite;

        // Load default explosion sound if none is assigned
        if (explosionSound == null)
            explosionSound = Resources.Load<AudioClip>("pomm/bomba");

        // Load default explosion sprites if none are assigned
        if (explosionSprites == null || explosionSprites.Length == 0)
        {
            explosionSprites = new Sprite[3];
            explosionSprites[0] = Resources.Load<Sprite>("pomm/pomm2_2");
            explosionSprites[1] = Resources.Load<Sprite>("pomm/pomm3_3");
            explosionSprites[2] = Resources.Load<Sprite>("pomm/pomm4_4");
        }
    }

    void Update()
    {
        if (isExploding)
            return;

        timer -= Time.deltaTime;

        // Update countdown display if assigned
        if (countdownDisplay != null)
            countdownDisplay.text = Mathf.Ceil(timer).ToString();

        // Trigger explosion when time is up
        if (timer <= 0)
            StartCoroutine(HandleExplosion());
    }

    IEnumerator HandleExplosion()
    {
        isExploding = true;
        Debug.Log("Bomb exploded!");

        // Play the explosion sound
        if (explosionSound != null)
            audioSource.PlayOneShot(explosionSound);

        // Affect the player as in the original bomb:
        GameObject.Find("puusad").SendMessage("Explode", transform);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // Play explosion animation if sprites are provided
        if (explosionSprites != null && explosionSprites.Length > 0 && sr != null)
        {
            for (int i = 0; i < explosionSprites.Length; i++)
            {
                sr.sprite = explosionSprites[i];
                yield return new WaitForSeconds(explosionAnimationSpeed);
            }
        }
        else
        {
            // Wait briefly if no explosion sprites are provided
            yield return new WaitForSeconds(0.5f);
        }

        // Restore the original sprite for the next cycle
        if (sr != null)
            sr.sprite = originalSprite;

        // Reset the timer and explosion flag for repeating explosions
        timer = countdownTime;
        isExploding = false;
    }
}
