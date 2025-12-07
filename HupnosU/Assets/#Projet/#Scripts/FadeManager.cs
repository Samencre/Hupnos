using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public CanvasGroup CanvasG;
    public float fadeDuration = 0.5f;
    public static FadeManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        CanvasG.alpha = 1;
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.captureDeltaTime;
            CanvasG.alpha = 1 - (time / fadeDuration);
            yield return null;
        }
        CanvasG.alpha = 0;
    }

    public IEnumerator FadeOut()
    {
        CanvasG.alpha = 0;
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.captureDeltaTime;
            CanvasG.alpha = time / fadeDuration;
            yield return null;
        }
        CanvasG.alpha = 1;
    }
}
