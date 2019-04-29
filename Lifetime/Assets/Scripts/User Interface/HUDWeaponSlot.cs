using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDWeaponSlot : MonoBehaviour
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI ammoCount;

    [SerializeField] private Color regularColor;
    [SerializeField] private Color selectedColor;

    public void SelectSlot()
    {
        slotImage.color = selectedColor;
    }

    public void UnselectSlot()
    {
        slotImage.color = regularColor;
    }

    public void UpdateRangedSlot(Weapon weapon)
    {
        if (!weapon)
        {
            return;
        }

        /* TODO: Add icon to weapons
        weaponImage.sprite = weapon.sprite; */

        /* TODO: Add ammo count to ranged weapons
        ammoCount.text = weapon.ammo */
    }

    public void UpdateMeleeSlot(Weapon weapon)
    {
        if (!weapon)
        {
            return;
        }

        /* TODO: Add icon to weapons
        weaponImage.sprite = weapon.sprite; */

        ammoCount.text = "";
    }

    public void UpdateSpecialSLot(Weapon weapon)
    {
        if (!weapon)
        {
            return;
        }

        /* TODO: Add icon to weapons
        weaponImage.sprite = weapon.sprite; */

        ammoCount.text = "";
    }
}
