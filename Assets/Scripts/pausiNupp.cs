using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausiNupp : MonoBehaviour
{
    public GameObject panelToShow;
    public GameObject pauseButton; // <-- Add this in the Inspector

    public void OpenPanel()
    {
        panelToShow.SetActive(true);
        pauseButton.SetActive(false); // Hide pause button
        Time.timeScale = 0f; // Pause game
    }

    public void ClosePanel()
    {
        panelToShow.SetActive(false);
        pauseButton.SetActive(true); // Show pause button
        Time.timeScale = 1f; // Resume game
    }

    public void onHomeButtonClicked()
    {
        Debug.Log("Home button clicked");
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

}
