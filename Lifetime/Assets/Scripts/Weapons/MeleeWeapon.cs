using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public bool isAuto;

    private bool readyToFire = true;

    public void Fire()
    {

    }
}
