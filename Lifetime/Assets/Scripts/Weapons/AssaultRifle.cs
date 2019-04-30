using System.Collections;
using UnityEngine;

public class AssaultRifle : Weapon, IWeapon
{
    [SerializeField] private int ammoCount = 20;
    protected bool readyToFire = true;
    [SerializeField] private ParticleSystem ps;

    private RaycastHit2D hit;

    public override void Fire(Vector3 playerPosition, Vector3 directionVector, float damageModifier, float knockbackModifier)
    {
        if (readyToFire)
        {
            ps.Emit(1);

            player.playerAnimator.Play("auto-fire");
            
            audioSource.PlayOneShot(fireAudioClips[Random.Range(0, fireAudioClips.Length)]);
            
            // Deplete ammo
            ammoCount--;
            if (ammoCount <= 0)
            {
                player.SwapActiveWeapon(3);
                Destroy(this);
            }

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