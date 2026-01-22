
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameCompleted : MonoBehaviour
{
    public GameObject gameCompletePanel;
    public GameObject finishBar;
    private Vector3 initialBarPosition;
    private bool barInitialized = false;
    private bool hasCompleted = false;

    /*
        void Start()
        {
            if (gameCompletePanel != null)
            {
                gameCompletePanel.SetActive(false);
            }

            if (finishBar != null)
            {
                finishBar.SetActive(false); // ensure it’s hidden initially
            }
        }
    */
    void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.CompareTag("Player"))
        {
            CompleteGame();
            // Disable instead of destroying to avoid coroutine error
            if (finishBar != null)
                finishBar.SetActive(false);

        }
    }

    public void SpawnBar()
    {
        if (finishBar != null)
        {
            if (!barInitialized)
            {
                initialBarPosition = finishBar.transform.position;
                barInitialized = true;
            }

            finishBar.transform.position = initialBarPosition;
            finishBar.SetActive(true);
            StartCoroutine(AnimateBar());
        }
    }


    public void CompleteGame()
    {
        if (hasCompleted) return;

        hasCompleted = true;

        if (gameCompletePanel != null)
        {
            gameCompletePanel.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Game ended");
            AudioManager.instance.Play("Theme");
            AudioManager.instance.Stop("Background");
        }
    }


    private IEnumerator AnimateBar()
    {
        float animationTime = 2f;
        float elapsedTime = 0f;

        Vector3 startPosition = finishBar != null ? finishBar.transform.position : Vector3.zero;
        Vector3 endPosition = startPosition + new Vector3(0f, -10f, 0f);

        while (elapsedTime < animationTime)
        {
            if (finishBar == null) yield break;

            finishBar.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / animationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (finishBar != null)
        {
            finishBar.transform.position = endPosition;
        }
    }


}

