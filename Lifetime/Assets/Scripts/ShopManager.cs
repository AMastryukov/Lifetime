using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public enum Attribute { DAMAGE, SPEED, LUCK, KNOCKBACK, STAMINA };
public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopPanel panel;

    [Header("Attribute Enums (just so you can see them XD)")]
    [SerializeField] private Attribute attribute;

    [Header("Upgradable Attributes")]
    [SerializeField] private Upgrade[] damageLevels;
    [SerializeField] private Upgrade[] speedLevels;
    [SerializeField] private Upgrade[] LuckLevels;
    [SerializeField] private Upgrade[] knockbackLevels;
    [SerializeField] private Upgrade[] staminaLevels;

    [Header("Melee Weapons")]
    [SerializeField] private WeaponUpgrade[] meleeWeapons;

    [Header("Ranged Weapons")]
    [SerializeField] private WeaponUpgrade[] rangeWeapons;

    [Header("Special Weapons")]
    [SerializeField] private WeaponUpgrade[] specialWeapons;

    private Player player;
    private Dictionary<Attribute, Upgrade[]> levels;
    private Dictionary<Attribute, int> currentLevel;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        levels = new Dictionary<Attribute, Upgrade[]>();
        currentLevel = new Dictionary<Attribute, int>();

    }

    public void Reset()
    {
        foreach (Attribute key in levels.Keys) {
            currentLevel[key] = 0;
        }
    }
    
    void Start()
    {
        levels.Add(Attribute.DAMAGE, damageLevels);
        levels.Add(Attribute.KNOCKBACK, knockbackLevels);
        levels.Add(Attribute.LUCK, LuckLevels);
        levels.Add(Attribute.SPEED, speedLevels);
        levels.Add(Attribute.STAMINA, staminaLevels);

        currentLevel.Add(Attribute.DAMAGE, 0);
        currentLevel.Add(Attribute.KNOCKBACK, 0);
        currentLevel.Add(Attribute.LUCK, 0);
        currentLevel.Add(Attribute.SPEED, 0);
        currentLevel.Add(Attribute.STAMINA, 0);

        // Update panel text
        panel.UpdateText(
            levels[Attribute.DAMAGE][currentLevel[Attribute.DAMAGE]].price,
            levels[Attribute.SPEED][currentLevel[Attribute.SPEED]].price,
            levels[Attribute.KNOCKBACK][currentLevel[Attribute.KNOCKBACK]].price);
    }
    
    public bool AttributeUpgradable(Attribute attr) {
        return currentLevel[attr] < levels[attr].Length;
    }

    public bool AttributeUpgradable(int attr)
    {
        return currentLevel[(Attribute)attr] < levels[(Attribute)attr].Length;
    }

    public bool EnoughLifetimeToUpgrade(Attribute attr) {
        return player.lifetime >= levels[attr][currentLevel[attr]].price;
    }

    public bool EnoughLifetimeToUpgrade(int attr)
    {
        return player.lifetime >= levels[(Attribute)attr][currentLevel[(Attribute)attr]].price;
    }

    public bool AttributePurchasable(Attribute attr) {
        return AttributeUpgradable(attr) && EnoughLifetimeToUpgrade(attr);
    }

    public bool AttributePurchasable(int attr)
    {
        return AttributeUpgradable(attr) && EnoughLifetimeToUpgrade(attr);
    }

    public void PurchaseAttribute(Attribute attribute) {
        if (!AttributePurchasable(attribute)) {
            return;
        }

        switch (attribute) {
            case Attribute.DAMAGE:
                PurchaseDamage();
                break;
            case Attribute.KNOCKBACK:
                PurchaseKnockback();
                break;
            case Attribute.LUCK:
                PurchaseLuck();
                break;
            case Attribute.SPEED:
                PurchaseSpeed();
                break;
            case Attribute.STAMINA:
                PurchaseStamina();
                break;
            default:
                break;
        }

        // Update panel text
        panel.UpdateText(
            levels[Attribute.DAMAGE][currentLevel[Attribute.DAMAGE]].price,
            levels[Attribute.SPEED][currentLevel[Attribute.SPEED]].price,
            levels[Attribute.KNOCKBACK][currentLevel[Attribute.KNOCKBACK]].price);
    }

    public void PurchaseAttribute(int attr)
    {
        PurchaseAttribute((Attribute) attr);
    }

    private void PurchaseDamage() {

        if (!AttributeUpgradable(Attribute.DAMAGE) || !EnoughLifetimeToUpgrade(Attribute.DAMAGE))
        {
            return;
        }

        Upgrade upgrade = levels[Attribute.DAMAGE][currentLevel[Attribute.DAMAGE]];
        player.damageModifier += upgrade.value;
        player.DrainLifetime(upgrade.price);
        currentLevel[Attribute.DAMAGE]++;
    }

    private void PurchaseLuck()
    {

    }

    private void PurchaseSpeed()
    {
        if (!AttributeUpgradable(Attribute.SPEED) || !EnoughLifetimeToUpgrade(Attribute.SPEED))
        {
            return;
        }

        Upgrade upgrade = levels[Attribute.SPEED][currentLevel[Attribute.SPEED]];
        player.speedModifier += upgrade.value;
        player.DrainLifetime(upgrade.price);
        currentLevel[Attribute.SPEED]++;
    }

    private void PurchaseStamina()
    {

    }

    private void PurchaseKnockback()
    {
        if (!AttributeUpgradable(Attribute.KNOCKBACK) || !EnoughLifetimeToUpgrade(Attribute.KNOCKBACK))
        {
            return;
        }

        Upgrade upgrade = levels[Attribute.KNOCKBACK][currentLevel[Attribute.KNOCKBACK]];
        player.knockbackModifier += upgrade.value;
        player.DrainLifetime(upgrade.price);
        currentLevel[Attribute.KNOCKBACK]++;
    }

    public void PurchaseMeleeWeapon(string name)
    {
        if (!WeaponPurchasable(name)) return;

        for (int i = 0; i < meleeWeapons.Length; i++) if (meleeWeapons[i].name == name) {
                player.equipMeleeWeapon(meleeWeapons[i].prefab);
                meleeWeapons[i].purchased = true;
                player.DrainLifetime(meleeWeapons[i].price);
            };
    }

    public void PurchaseRangeWeapon(string name)
    {
        if (!WeaponPurchasable(name)) return;

        for (int i = 0; i < rangeWeapons.Length; i++) if (rangeWeapons[i].name == name)
            {
                player.equipRangeWeapon(rangeWeapons[i].prefab);
                rangeWeapons[i].purchased = true;
                player.DrainLifetime(rangeWeapons[i].price);
            };
    }

    public void PurchaseSpecialWeapon(string name)
    {
        if (!WeaponPurchasable(name)) return;

        for (int i = 0; i < specialWeapons.Length; i++) if (specialWeapons[i].name == name)
            {
                player.equipSpecialWeapon(meleeWeapons[i].prefab);
                specialWeapons[i].purchased = true;
                player.DrainLifetime(specialWeapons[i].price);
            };
    }

    private bool WeaponNotPurchased(string name) {
        foreach (WeaponUpgrade up in meleeWeapons) if (up.name == name) return !up.purchased;

        foreach (WeaponUpgrade up in rangeWeapons) if (up.name == name) return !up.purchased;

        foreach (WeaponUpgrade up in specialWeapons) if (up.name == name) return !up.purchased;

        return false;
    }

    private bool EnoughLifetimeForWeapon(string name)
    {
        foreach (WeaponUpgrade up in meleeWeapons) if (up.name == name) return player.lifetime >= up.price;

        foreach (WeaponUpgrade up in rangeWeapons) if (up.name == name) return player.lifetime >= up.price;

        foreach (WeaponUpgrade up in specialWeapons) if (up.name == name) return player.lifetime >= up.price;

        return false;
    }

    public bool WeaponPurchasable(string name)
    {
        foreach (WeaponUpgrade up in meleeWeapons) if (up.name == name) return !up.purchased && player.lifetime >= up.price;

        foreach (WeaponUpgrade up in rangeWeapons) if (up.name == name) return !up.purchased && player.lifetime >= up.price;

        foreach (WeaponUpgrade up in specialWeapons) if (up.name == name) return !up.purchased && player.lifetime >= up.price;

        return false;
    }
}

[System.Serializable]
public struct Upgrade
{
    public float value;
    public float price;
}

[System.Serializable]
public struct WeaponUpgrade
{
    public string name;
    public GameObject prefab;
    public float price;
    public bool purchased;
}
