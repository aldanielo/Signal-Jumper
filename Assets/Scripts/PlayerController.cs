using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float upwardForce = 1f;
    public float sideSpeed = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.AddForce(Vector3.left * sideSpeed, ForceMode.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.AddForce(Vector3.right * sideSpeed, ForceMode.Impulse);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.AddForce(Vector3.down * sideSpeed, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        rb.velocity *= 0.99f; // Damping

        if (transform.position.y < -12f)
        {
            RespawnPlayer();
        }
    }


    void RespawnPlayer()
    {
        // Deduct points
        GameManager.instance.DeductScore(5);

        // Respawn the player at a safe height
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.velocity = Vector3.zero;
    }

}
