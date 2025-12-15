using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public float fadeDuration = 2f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (fadeCanvas != null)
            fadeCanvas.alpha = 0f;
    }

    public void FadeToScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            return;
        }
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = t / fadeDuration;
            yield return null;
        }
        fadeCanvas.alpha = 1f;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }
}


