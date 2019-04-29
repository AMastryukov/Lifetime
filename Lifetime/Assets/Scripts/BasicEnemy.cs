using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    private float directionChangeInterval = 1;

    private Vector2 targetDirection;

    [SerializeField] private float wanderSpeed;
    [SerializeField] private float attackCooldown;

    [SerializeField] private CircleCollider2D attackArea;
    private float attackRadius;


    [SerializeField] protected Transform target;
    [SerializeField] private ParticleSystem ps;

    [SerializeField] private LayerMask sightMask;

    private bool onCooldown;
    private bool agitated;

    private GameObject bloodSystem;


    // Start is called before the first frame update
    void Start()
    {
        bloodSystem = GameObject.FindGameObjectWithTag("BloodParticleSystem");
        audioSource = GameObject.FindGameObjectWithTag("SoundSystem").GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        movement = GetComponent<Movement>();
        attackRadius = attackArea.radius;
        attackArea.enabled = false;
        NewHeadingRoutine();
        StartCoroutine(NewHeading());
        agitated = false;
        onCooldown = false;

        AssignSkin();

        movement.moveSpeed = wanderSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (onCooldown) {
            movement.SetDirectionVector(Vector2.zero);
            return;
        }

        if (Vector2.Distance(target.position, transform.position) <= attackRadius)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, attackRadius, sightMask);
            //Debug.DrawRay(playerPosition, transform.position - target.position);
            if (!hit.collider)
            {
                return;
            }

            if (hit.collider.tag == "Player") {
                
                agitated = true;
                movement.moveSpeed = speed;
            }
            if(agitated)
            movement.SetDirectionVector(target.transform.position - transform.position);
            movement.SetLookPosition(target.transform.position);
        }
        else {
            agitated = false;
            movement.moveSpeed = wanderSpeed;
        }

        if (agitated) {
            movement.SetDirectionVector(target.transform.position - transform.position);
            movement.SetLookPosition(target.transform.position);
        } else {
            // Wander
            Vector3 newDirection = Vector3.Slerp(transform.up, targetDirection, 0.1f);
            movement.SetDirectionVector(newDirection);
            movement.SetLookPosition(transform.position + newDirection);

            //var forward = transform.TransformDirection(Vector3.forward);
            //controller.SimpleMove(forward);
        }
    }

    public override void TakeDamage(float damage)
    {
        // Spawn blood particles
        bloodSystem.transform.position = transform.position;
        bloodSystem.GetComponent<ParticleSystem>().Play();

        base.TakeDamage(damage);
    }

    /// <summary>
	/// Repeatedly calculates a new direction to move towards.
	/// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
	/// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
	/// Calculates a new direction to move towards.
	/// </summary>
	void NewHeadingRoutine()
    {

        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        targetDirection = new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onCooldown) return;
        if (collision.gameObject.tag == "Player") {
            ((IDamageable)collision.gameObject.GetComponent<MonoBehaviour>()).TakeDamage(damage);
            StartCoroutine(AttackCoolDown());
            
        }
    }

    private IEnumerator AttackCoolDown()
    {
        onCooldown = true;

        yield return new WaitForSeconds(attackCooldown);

        onCooldown = false;
    }
}
