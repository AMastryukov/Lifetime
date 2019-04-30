using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableCrate : MonoBehaviour, IDamageable
{
    [SerializeField] private float hitpoints = 50f;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private UnityEvent onBreak;

    [SerializeField] private GameObject[] weaponPrefabs;
    
    private float damageThresholdMedium, damageThresholdLow;

    private void Start()
    {
        // Calculate damage thresholds
        damageThresholdMedium = hitpoints / 2f;
        damageThresholdLow = hitpoints / 5f;

        audioSource = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<AudioSource>();
    }

    public void TakeDamage(float damage)
    {
        hitpoints -= damage;

        audioSource.PlayOneShot(hitSound);

        // Change sprite based on damage dealt to the box
        if (hitpoints <= damageThresholdMedium && hitpoints > damageThresholdLow)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (hitpoints <= damageThresholdLow)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
        }

        // If the box is destroyed
        if (hitpoints <= 0f)
        {
            onBreak.Invoke();

            // Create a random weapon and give it to the player
            GameObject weaponDrop = Instantiate(weaponPrefabs[Random.Range(0, weaponPrefabs.Length)]);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().equipRangeWeapon(weaponDrop);

            audioSource.PlayOneShot(breakSound);
            gameObject.SetActive(false);
        }
    }

    public void TakeCriticalDamage(float damage) {
        TakeDamage(damage);
    }
}
