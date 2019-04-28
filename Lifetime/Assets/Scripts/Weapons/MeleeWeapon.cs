using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    
    public float damage;
    public float range;
    public float fireRate;
    public float knockback;
    public bool isAuto;

    [SerializeField] private LayerMask hitMask;

    private bool readyToFire = true;
    private RaycastHit2D[] circleCastHits;

    public void Fire(Vector3 playerPosition, Vector3 directionVector)
    {
        if (readyToFire)
        {
            // Cast circle sweeping in front of the player
            circleCastHits = Physics2D.CircleCastAll(
                playerPosition + directionVector.normalized * 0.25f,
                0.5f,
                directionVector,
                range,
                hitMask);

            // Hit all objects
            foreach (RaycastHit2D hit in circleCastHits)
            {
                // If object can be pushed, push it
                if (hit.collider.GetComponent<Rigidbody2D>())
                {
                    hit.collider.GetComponent<Rigidbody2D>().AddForceAtPosition(directionVector * knockback, hit.point);
                    //hit.collider.GetComponent<Rigidbody2D>().velocity = directionVector * knockback;
                }

                // Damage object if it can be damaged
                if (hit.collider.GetComponent<MonoBehaviour>() is IDamageable)
                {
                    ((IDamageable)hit.collider.GetComponent<MonoBehaviour>()).TakeDamage(damage);
                }
            }

            // Put the weapon on cooldown
            StartCoroutine(WeaponCooldown());
        }
    }

    private IEnumerator WeaponCooldown()
    {
        readyToFire = false;

        yield return new WaitForSeconds(1f / fireRate);

        readyToFire = true;
    }
}
