using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float upwardForce = 10f;
    public float sideSpeed = 5f;
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
    }

    void FixedUpdate()
    {
        rb.velocity *= 0.99f; // Damping

        if (transform.position.y < -12f)
        {
            GameManager.instance.GameOver();
        }
    }
}
