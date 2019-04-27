using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public bool isAuto;

    private bool readyToFire = true;

    public void Fire(Vector3 playerPosition, Vector3 directionVector)
    {

    }

    public void Reload()
    {

    }
}