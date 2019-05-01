using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public float health = 100;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float speed = 10;

    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Sprite[] sprites;

    [SerializeField] protected AudioClip[] hitmarkerSounds;

    protected AudioSource audioSource;
    protected Movement movement;

    protected void AssignSkin()
    {
        // Set a random sprite for the enemy
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        audioSource.PlayOneShot(hitmarkerSounds[Random.Range(0, hitmarkerSounds.Length)]);

        if (health <= 0) {
            Die();
        }
    }

    public virtual void TakeCriticalDamage(float damage) {
        TakeDamage(damage);
    }

    public void Die() {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().RegisterEnemyDeath();
        Destroy(gameObject);
    }
}
