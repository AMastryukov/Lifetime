using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] public float health = 100;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float speed = 10;

    protected Movement movement;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
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
