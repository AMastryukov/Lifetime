using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDPanel : MonoBehaviour
{

    [SerializeField] private Canvas panelCanvas;

    [SerializeField] private TextMeshProUGUI lifetimeText;
    [SerializeField] private TextMeshProUGUI enemyText;

    [SerializeField] private HUDWeaponSlot meleeSlot;
    [SerializeField] private HUDWeaponSlot rangedSlot;
    [SerializeField] private HUDWeaponSlot specialSlot;
    
    public void SelectWeaponSlot(int slot)
    {
        switch(slot)
        {
            // Select special slot, unselect others
            case 1:
                specialSlot.SelectSlot();
                rangedSlot.UnselectSlot();
                meleeSlot.UnselectSlot();
                break;

            // Select ranged slot, unselect others
            case 2:
                specialSlot.UnselectSlot();
                rangedSlot.SelectSlot();
                meleeSlot.UnselectSlot();
                break;
            
            // Select melee slot, unselect others
            case 3:
                specialSlot.UnselectSlot();
                rangedSlot.UnselectSlot();
                meleeSlot.SelectSlot();
                break;

            // Default to selecting melee slot 
            default:
                specialSlot.UnselectSlot();
                rangedSlot.UnselectSlot();
                meleeSlot.SelectSlot();
                break;
        }
    }

    public void UpdatePlayerWeapons(Player player)
    {
        specialSlot.UpdateSpecialSLot(player.specialWeapon);
        rangedSlot.UpdateRangedSlot(player.rangedWeapon);
        meleeSlot.UpdateMeleeSlot(player.meleeWeapon);
    }

    public void UpdateLifetime(float lifetime)
    {
        lifetimeText.text = string.Format("{0:00}:{1:00}", (int)lifetime / 60, (int)lifetime % 60);
    }

    public void UpdateEnemyCount(int enemyCount)
    {
        enemyText.text = enemyCount + " enemies";
    }

    public void OpenPanel()
    {
        panelCanvas.enabled = true;
    }

    public void ClosePanel()
    {
            panelCanvas.enabled = false;
    }
}
