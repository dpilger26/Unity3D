using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float splashScreenWaitSeconds = 3f;

    private void Start()
    {
        StartCoroutine(LoadStartMenu());
    }

    private IEnumerator LoadStartMenu()
    {
        yield return new WaitForSecondsRealtime(splashScreenWaitSeconds);
        SceneManager.LoadScene(1);
    }
}
