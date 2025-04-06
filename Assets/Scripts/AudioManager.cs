using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip[] levelMusic;
    public float fadeDuration = 1.5f;

    private bool hasPlayedFirstTrack = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource.volume = 1f;
            PlayLevelMusic(0); // Start first track immediately
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLevelMusic(int index)
    {
        if (index < levelMusic.Length && levelMusic[index] != null)
        {
            if (!hasPlayedFirstTrack)
            {
                audioSource.clip = levelMusic[index];
                audioSource.Play();
                hasPlayedFirstTrack = true;
            }
            else
            {
                StartCoroutine(FadeToNewClip(levelMusic[index]));
            }
        }
    }

    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        if (audioSource.clip == newClip) yield break; // Skip if already playing

        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
