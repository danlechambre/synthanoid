using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private Camera cam;
    private GameObject ball;

#pragma warning disable CS0649
    [SerializeField] // CS0649
    private float minX, maxX;
#pragma warning restore CS0649

    [SerializeField]
    private bool autoplayEnabled = false;

    private void Start()
    {
        cam = Camera.main;
        ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }

        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float xPos;
        float yPos;

        if (autoplayEnabled)
        {
            xPos = Mathf.Clamp(ball.transform.position.x, minX, maxX);
            yPos = transform.position.y;

            transform.position = new Vector2(xPos, yPos);
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPoint = cam.ScreenToWorldPoint(mousePos);

            xPos = Mathf.Clamp(worldPoint.x, minX, maxX);
            yPos = transform.position.y;

            transform.position = new Vector2(xPos, yPos);
        }
    }
}
