using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifetimeText;
    [SerializeField] private TextMeshProUGUI enemyText;
    
    // TODO: Hook up weapon panel

    public void UpdateLifetime(float lifetime)
    {
        lifetimeText.text = string.Format("{0:00}:{1:00}", (int)lifetime / 60, (int)lifetime % 60);
    }

    public void UpdateEnemyCount(int enemyCount)
    {
        enemyText.text = enemyCount + " enemies";
    }
}
