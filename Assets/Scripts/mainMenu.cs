using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("GameScene");
    }

    public void onQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();

        // This line is just for testing in the editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
