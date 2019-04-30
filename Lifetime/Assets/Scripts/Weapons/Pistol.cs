using System.Collections;
using UnityEngine;

public class Pistol : Weapon, IWeapon
{
    protected bool readyToFire = true;
    [SerializeField] private ParticleSystem ps;

    private RaycastHit2D hit;
    
    public override void Fire(Vector3 playerPosition, Vector3 directionVector, float damageModifier, float knockbackModifier)
    {
        if (readyToFire)
        {
            ps.Emit(1);

            player.playerAnimator.Play("pistol-fire");

            audioSource.PlayOneShot(fireAudioClips[Random.Range(0, fireAudioClips.Length)]);
            
            // Put the weapon on cooldown
            StartCoroutine(WeaponCooldown());

            hit = Physics2D.Raycast(playerPosition, directionVector, range, hitMask);
            if (!hit.collider)
            {
                return;
            }

            // Damage object if it can be damaged
            if (hit.collider.GetComponent<MonoBehaviour>() is IDamageable)
            {
                ((IDamageable)hit.collider.GetComponent<MonoBehaviour>()).TakeDamage(damage * damageModifier);
            }

            // Push object based on knockback
            if (hit.collider.GetComponent<Rigidbody2D>())
            {
                // hit.collider.GetComponent<Rigidbody2D>().velocity = directionVector * knockback;
                hit.collider.GetComponent<Rigidbody2D>().AddForceAtPosition(directionVector * knockback * knockbackModifier, hit.point);
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