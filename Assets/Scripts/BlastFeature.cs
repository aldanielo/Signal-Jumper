using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastFeature : MonoBehaviour
{
    public int pointsToDeduct = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.DeductScore(pointsToDeduct);
            Destroy(gameObject);
        }
    }

}
