using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFeature : MonoBehaviour
{
    public int pointsToDeduct = 5;

    private void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.Play("Blast");
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.RemoveScore(pointsToDeduct);
            Destroy(gameObject);
        }
    }

}
