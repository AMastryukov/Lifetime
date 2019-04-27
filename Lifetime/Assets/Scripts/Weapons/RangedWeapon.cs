using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public bool isAuto;

    private bool readyToFire = true;

    public void Fire()
    {

    }

    public void Reload()
    {

    }
}