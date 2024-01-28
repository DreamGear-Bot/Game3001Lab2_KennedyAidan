using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 targetPosition = Vector3.zero;
    Rigidbody2D rb;

    Transform target;
    Vector2 seek;

    void Start()
    {
        Vector2 A = new Vector2(2.0f, 3.0f);    // Start position
        Vector2 B = new Vector2(10.0f, 15.0f);  // End position
        Vector2 AB = B - A;                     // Line from start to end
        Vector2 N1 = AB.normalized;
        Vector2 N2 = AB / Mathf.Sqrt(AB.x * AB.x + AB.y * AB.y);
        float length1 = N1.magnitude;
        float length2 = N2.magnitude;
        Debug.Log(N1);  // Automatic normalization
        Debug.Log(N2);  // Manual normalization
        Debug.Log(length1);
        Debug.Log(length2);

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("planet").transform;
    }

    void Update()
    {
        // Check for mouse input.
        if (Input.GetMouseButton(0))
        {
            // Convert mouse position to world position.
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0.0f; // Ensure the Z-coordinate is correct for a 2D game  .     
        }

        //Vector3 direction = (targetPosition - transform.position).normalized;

        // Move towards the target position.
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Manual implementation of the above
        //float translation = Mathf.Min(moveSpeed * Time.deltaTime, (targetPosition - transform.position).magnitude);
        //transform.position += (targetPosition - transform.position).normalized * translation;

        // Homework: change this to a generic Seek function.
        // You should supply two GameObjects -- a seeker, and a target
        // The seeker must contain a rigidbody, and a maximum speed. All the target needs is a position.
        if (target)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            seek = direction;
            targetPosition.z = 0.0f;
            //rb.velocity = new Vector2(seek.x, seek.y) * moveSpeed;
        }

        Vector2 currentVelocity = rb.velocity;
        Vector2 desiredVelocity = seek * moveSpeed;
        rb.AddForce(desiredVelocity - currentVelocity);

        // Rotate to look at the target position.
        LookAt2D(targetPosition);
    }

    //void SeekTarget<T>()
    //{
    //    Type target = typeof(T);
    //    Vector2 currentVelocity = rb.velocity;
    //    Vector2 desiredVelocity = seek * moveSpeed;
    //    rb.AddForce(desiredVelocity - currentVelocity);
    //}

    void LookAt2D(Vector3 target)
    {
        Vector3 lookDirection = target - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
    }
}
