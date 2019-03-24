using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // configuration parameters
    [SerializeField] [Tooltip("In seconds")] float splashScreenWaitSeconds = 3f;
    [SerializeField] [Tooltip("In seconds")] float levelLoadDelay = 2f;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            StartCoroutine(LoadStartMenu());
        }
    }

    private IEnumerator LoadStartMenu()
    {
        yield return new WaitForSecondsRealtime(splashScreenWaitSeconds);
        SceneManager.LoadScene(1);
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelCoroutine());
    }

    private IEnumerator RestartLevelCoroutine()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
