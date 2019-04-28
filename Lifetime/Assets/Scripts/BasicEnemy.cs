using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{

    private float directionChangeInterval = 1;

    private Vector2 targetDirection;

    [SerializeField] private CircleCollider2D attackArea;
    private float attackRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        attackRadius = attackArea.radius;
        attackArea.enabled = false;
        NewHeadingRoutine();
        StartCoroutine(NewHeading());
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(target.position, transform.position) <= attackRadius)
        {
            movement.SetDirectionVector(target.transform.position - transform.position);
            movement.SetLookPosition(target.transform.position);
        }
        else {

            // Wander
            /*Vector3 newDirection = Vector3.Slerp(transform.forward, targetDirection, 0.1f);
            movement.SetDirectionVector(newDirection);
            movement.SetLookPosition(transform.position + newDirection);*/

            /*var forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward);*/
        }

        
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
}
