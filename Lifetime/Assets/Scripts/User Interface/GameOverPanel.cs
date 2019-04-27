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
        if (panelCanvas.enabled)
        {
            panelCanvas.enabled = false;
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            ClosePanel();
        }
    }
}
