using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource audioSource;
    public AudioClip[] levelMusic; // Assign music clips for each level here

    public void Start() 
    {
        PlayLevelMusic(0);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayLevelMusic(int levelIndex)
    {
        if (levelIndex < levelMusic.Length && levelMusic[levelIndex] != null)
        {
            audioSource.clip = levelMusic[levelIndex];
            audioSource.Play();
        }
    }
}
