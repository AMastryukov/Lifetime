using UnityEngine;
using UnityEngine.Events;

public class ShopPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Canvas panelCanvas;
    [SerializeField] private UnityEvent closeShop;
    
    public void OpenPanel()
    {
        panelCanvas.enabled = true;
    }

    public void ClosePanel()
    {
        panelCanvas.enabled = false;
    }
}
