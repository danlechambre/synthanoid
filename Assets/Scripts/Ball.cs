using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private PaddleController paddle;
    private AudioSource audioSource;

#pragma warning disable CS0649
    [SerializeField]
    private AudioClip[] clips;
#pragma warning restore CS0649

    private Rigidbody2D rb;

#pragma warning disable CS0649
    [Range(0.1f, 15.0f)][SerializeField]
    private float launchVelocityX, launchVelocityY;
    [Range(5.0f, 15.0f)][SerializeField]
    float maxVelocity = 10.0f;
    [Range(0.1f, 10.0f)][SerializeField]
    float minXVelocity = 3.0f;
    [Range(0.1f, 10.0f)][SerializeField]
    float minYVelocity = 5.0f;
    [Range(0.1f, 3.0f)][SerializeField]
    private float velocityAdjustmentX = 1.5f;
    [Range(0.1f, 3.0f)]
    [SerializeField]
    private float velocityAdjustmentY = 3.0f;
    [Range(0.1f, 0.9f)][SerializeField]
    private float zeroVelocityAdjustment = 0.5f;
#pragma warning restore CS0649

    private Vector2 paddleToBallVector;
    public bool launched = false;
    

    private void Start()
    {
        paddle = GameObject.Find("Paddle").GetComponent<PaddleController>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        paddleToBallVector = transform.position - paddle.transform.position;
    }

    private void Update()
    {
        if (!launched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(launchVelocityX, launchVelocityY);
            launched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (launched)
        {
            AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
            RegulateVelocity();
        }
    }

    private void RegulateVelocity()
    {
        if (rb.velocity.x > -minXVelocity && rb.velocity.x < minXVelocity)
        {
            PhysicsShuntX();
        }
        else if (rb.velocity.y > -minYVelocity && rb.velocity.y < minYVelocity)
        {
            PhysicsShuntY();
        }    
    }

    private void PhysicsShuntX()
    {
        float velocityX = rb.velocity.x * velocityAdjustmentX;
        float velocityY = rb.velocity.y;
        Vector2 newVelocity = new Vector2(Mathf.Clamp(velocityX, -maxVelocity, maxVelocity), Mathf.Clamp(velocityY, -maxVelocity, maxVelocity));

        // Hard shunt if stuck perfectly on X
        if (rb.velocity.x == 0.0f)
        {
            newVelocity.x += (velocityAdjustmentX * zeroVelocityAdjustment);
        }

        rb.velocity = newVelocity;
    }

    private void PhysicsShuntY()
    {
        float velocityX = rb.velocity.x;
        float velocityY = rb.velocity.y * velocityAdjustmentY;
        Vector2 newVelocity = new Vector2(Mathf.Clamp(velocityX, -10.0f, 10.0f), Mathf.Clamp(velocityY, -maxVelocity, maxVelocity));

        // Hard shunt if stuck perfectly on Y
        if (rb.velocity.y == 0.0f)
        {
            newVelocity.y -= (velocityAdjustmentY * zeroVelocityAdjustment);
        }

        rb.velocity = newVelocity;
    }
}
