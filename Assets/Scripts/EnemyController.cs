using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float chaseSpeed = 5.0f;
    public float visionRange = 10.0f;
    private Transform playerTransform;
    private bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
        }
    }

    bool CanSeePlayer()
    {
        Vector3 playerDirection = playerTransform.position - transform.position;
        float distanceToPlayer = playerDirection.magnitude;

        if (distanceToPlayer <= visionRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection.normalized, visionRange);

            if (hit.collider != null)
            {
                Debug.Log("Hit object: " + hit.collider.name);
                if (hit.collider.CompareTag("Obstacle"))
                    return false;

                if (hit.collider.CompareTag("Player"))
                    return true;

            }
        }

        return false;
    }

    void ChasePlayer()
    {
        if (isChasing)
        {
            Vector3 moveDirection = (playerTransform.position - transform.position).normalized;
            transform.position += moveDirection * chaseSpeed * Time.deltaTime;
        }
    }
}
