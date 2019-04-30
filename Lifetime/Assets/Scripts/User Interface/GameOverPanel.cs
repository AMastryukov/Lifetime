using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Canvas panelCanvas;

    public void OpenPanel()
    {
        panelCanvas.enabled = true;
    }

    public void ClosePanel()
    {
        panelCanvas.enabled = false;
    }

    private void Update()
    {
        if (panelCanvas.enabled && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadSceneAsync("MainMenu");
            panelCanvas.enabled = false;
        }
    }
}
