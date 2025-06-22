using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    public float multiplier = 2f;
    public float duration = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.ActivateScoreMultiplier(multiplier, duration);
            Destroy(gameObject); // Destroy the power-up after it's collected
        }
    }
}
