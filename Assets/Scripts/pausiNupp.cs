using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pausiNupp : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelToShow;

    public void ClosePanel()
    {
        panelToShow.SetActive(false);
    }

    public void OpenPanel()
    {
        panelToShow.SetActive(true);
    }

    public void toggleFullscreen()
    {
        if (Screen.fullScreen == false)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
}
