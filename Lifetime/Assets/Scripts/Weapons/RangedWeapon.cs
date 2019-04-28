using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public bool isAuto;
    public float knockback;

    protected bool readyToFire = true;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private LayerMask hitMask;

    private RaycastHit2D hit;


    public virtual void Fire(Vector3 playerPosition, Vector3 directionVector)
    {
        if (readyToFire)
        {
            ps.Emit(1);

            hit = Physics2D.Raycast(playerPosition, directionVector, range, hitMask);
            if (!hit.collider)
            {
                return;
            }

            if (hit.collider.GetComponent<MonoBehaviour>() is IDamageable)
            {
                ((IDamageable)hit.collider.GetComponent<MonoBehaviour>()).TakeDamage(damage);
                hit.collider.GetComponent<Rigidbody2D>().velocity = directionVector * knockback;
            }

            // Put the weapon on cooldown
            StartCoroutine(WeaponCooldown());
        }

    }

    public void Reload()
    {

    }

    private IEnumerator WeaponCooldown()
    {
        readyToFire = false;

        yield return new WaitForSeconds(1f / fireRate);

        readyToFire = true;
    }
}