using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float upwardForce = 1f;
    public float sideSpeed = 1f;
    private Rigidbody rb;
    public int points = 5;

    public bool isRespawning = false;
    int delay = 2;

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
        if (!isRespawning)
        {
            rb.velocity *= 0.99f; // Damping
        }
        

        if (transform.position.y < -12f && !isRespawning)
        {
            StartCoroutine(ReSpawnAfterDelay(delay));
        }
    }

    
    void RespawnPlayer()
    {
        // Deduct points
        GameManager.instance.RemoveScore(points);

        // Respawn the player at a safe height
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.velocity = Vector3.zero;
    }

    IEnumerator ReSpawnAfterDelay(float delay)
    {
        isRespawning = true;
        // Optional: hide the ball or disable controls here
        rb.velocity = Vector3.zero;

        GameManager.instance.RemoveScore(points);

        yield return new WaitForSeconds(delay);

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        rb.velocity = Vector3.zero;

        isRespawning = false;

        // Optional: re-enable visuals or input here
    }

}
