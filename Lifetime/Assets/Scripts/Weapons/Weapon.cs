using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public int clipSize;
    public float reloadTime;
    public float knockback;
    public bool isAutomatic;


    [SerializeField] protected LayerMask hitMask;

    public virtual void Fire(Vector3 origin, Vector3 direction, float damageModifier, float knockbackModifier)
    {
        
    }


}
