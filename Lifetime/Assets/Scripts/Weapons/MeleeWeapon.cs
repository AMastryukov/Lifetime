using System.Collections;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IWeapon
{
    
    public float damage;
    public float range;
    public float fireRate;
    public float knockback;
    public bool isAuto;

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
                ~(1 << LayerMask.NameToLayer("Player")));

            // Hit all objects
            foreach (RaycastHit2D hit in circleCastHits)
            {


                // If object can be pushed, push it
                if (hit.collider.GetComponent<Rigidbody2D>())
                {
                    //hit.collider.GetComponent<Rigidbody2D>().AddForce(directionVector * knockback, ForceMode2D.Force);
                    hit.collider.GetComponent<Rigidbody2D>().velocity = directionVector * knockback;
                    
                }

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
