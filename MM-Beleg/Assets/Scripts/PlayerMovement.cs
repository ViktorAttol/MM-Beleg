using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles movement functionality of Player Entity. 
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 600f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 mousePos;

    void Start()
    {
        moveSpeed += 150 * LevelDataHandler.additionalSpeed;
    }
    
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Moves Player Entity base on keyboard input. 
    /// Sets LookDirection based on mouse position.
    /// Movement speed can be increased through purchased upgrades.
    /// </summary>
    /// <param name="speed"> Scaling multiplier for movement speed. </param>
    public void Move(float speed)
    {
        float tempSpeed = moveSpeed * speed;

        if (Input.GetAxisRaw("Horizontal") > 0.01f)
        {
            Vector2 dir;
            dir.x = tempSpeed;
            dir.y = 0;
            rb.AddForce(dir);
        }
        if (Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            Vector2 dir;
            dir.x = -tempSpeed;
            dir.y = 0;
            rb.AddForce(dir);
        }
        if (Input.GetAxisRaw("Vertical") > 0.01f)
        {
            Vector2 dir;
            dir.x = 0;
            dir.y = tempSpeed;
            rb.AddForce(dir);
        }
        if (Input.GetAxisRaw("Vertical") < -0.01f)
        {
            Vector2 dir;
            dir.x = 0;
            dir.y = -tempSpeed;
            rb.AddForce(dir);
        }

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
