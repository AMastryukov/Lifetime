using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    public float damage;
    public float range;
    public float fireRate;
    public float reloadTime;
    public float knockback;
    public bool isAutomatic;
    
    public Player player;
    public AudioClip[] fireAudioClips;

    protected AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<AudioSource>();
    }

    [SerializeField] protected LayerMask hitMask;

    public virtual void Fire(Vector3 origin, Vector3 direction, float damageModifier, float knockbackModifier)
    {
        
    }


}
