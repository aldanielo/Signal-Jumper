using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static PlatformSpawner;

public class PlatformMover : MonoBehaviour
{
    public PlatformType platformType;
    public float speed;
    private bool hasBeenHit = false;


    private void Update()
    {
        // Move platform downwards
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destroy platform when it goes off-screen
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // You can add additional behavior for each platform type here
    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.Play("Bounce");

        // Check if the ball collided with this platform
        if (collision.gameObject.CompareTag("Player") && !hasBeenHit)
        {
            hasBeenHit = true; // Set flag to true

            //    Destroy(gameObject); // Destroys platform after hit
            switch (platformType)
            {
                case PlatformType.Bonus:
                case PlatformType.Speed:        
                case PlatformType.Safe:
                    GameManager.instance.AddScore(); // Add score
                    //Debug.Log("Safe platform hit!");
                    break;
                case PlatformType.Danger:
                    // Apply damage or penalty
                    GameManager.instance.RemoveScore();
                    //Debug.Log("Danger platform hit!");
                    break;
            }

            if (platformType == PlatformType.Speed)
            {
               // Debug.Log("Speed platform hit!");
                collision.gameObject.GetComponent<Rigidbody>().velocity *= 1.5f;
            }
        }
    }

}
