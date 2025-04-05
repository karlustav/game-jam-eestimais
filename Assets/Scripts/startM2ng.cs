using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startM2ng : MonoBehaviour
{
    public GameObject panel;  // Reference to the Panel GameObject

    void Start()
    {
        // Disable the panel at the start of the scene
        if (panel != null)
        {
            panel.SetActive(false); // This makes the panel invisible
        }
}
}
