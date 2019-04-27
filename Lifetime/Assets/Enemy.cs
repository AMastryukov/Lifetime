using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    [SerializeField] protected float health = 100;
    [SerializeField] protected float damage = 5;
    [SerializeField] protected float speed = 10;
    [SerializeField] protected Transform target;

    protected Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
