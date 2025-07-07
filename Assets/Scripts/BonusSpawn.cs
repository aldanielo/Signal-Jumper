using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    public float multiplier = 3f;
    public float duration = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.Play("Bonus");
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.ActivateScoreMultiplier(multiplier, duration);
            Destroy(gameObject); // Destroy the power-up after it's collected
        }
    }
}
