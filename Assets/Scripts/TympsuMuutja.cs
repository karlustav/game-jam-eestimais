using UnityEngine;

public class TympsuMuutja : MonoBehaviour
{
    public int soundIndex;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!hasTriggered && other.CompareTag("player")) 
        {
            Debug.Log("Music trigger activated once.");
            AudioManager.Instance.PlayLevelMusic(soundIndex);
            hasTriggered = true;
        }
    }
}
