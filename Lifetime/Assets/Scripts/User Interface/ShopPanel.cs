using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ShopPanel : MonoBehaviour, IPanel
{
    [SerializeField] private Canvas panelCanvas;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Player player;

    [SerializeField] private TextMeshProUGUI lifetimeRemainingText;

    [SerializeField] private TextMeshProUGUI damageUpgradeText;
    [SerializeField] private TextMeshProUGUI speedUpgradeText;
    [SerializeField] private TextMeshProUGUI knockbackUpgradeText;

    private void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
    }

    public void OpenPanel()
    {
        lifetimeRemainingText.text = "Lifetime: " + string.Format("{0:00}:{1:00}", (int)player.lifetime / 60, (int)player.lifetime % 60);
        panelCanvas.enabled = true;
    }

    public void UpdateText(float damageUpgradeCost, float speedUpgradeCost, float knockbackUpgradeCost)
    {
        lifetimeRemainingText.text = "Lifetime: " + string.Format("{0:00}:{1:00}", (int)player.lifetime / 60, (int)player.lifetime % 60);

        damageUpgradeText.text = "Damage Upgrade (-" + string.Format("{0:00}:{1:00}", (int)damageUpgradeCost / 60, (int)damageUpgradeCost % 60) + ")";
        speedUpgradeText.text = "Speed Upgrade (-" + string.Format("{0:00}:{1:00}", (int)speedUpgradeCost / 60, (int)speedUpgradeCost % 60) + ")";
        knockbackUpgradeText.text = "Knockback Upgrade (-" + string.Format("{0:00}:{1:00}", (int)knockbackUpgradeCost / 60, (int)knockbackUpgradeCost % 60) + ")";
    }

    public void ClosePanel()
    {
        panelCanvas.enabled = false;
    }
}
