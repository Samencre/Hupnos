using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHatchiManager : MonoBehaviour
{
    public CanvasGroup panel;      
    public TMP_Text textDisplay;   
    [TextArea(3,10)]
    public string[] lines;         
    public float fadeDuration = 1.5f; 
    public string nextSceneName;     

    void Start()
    {
        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        foreach (string line in lines)
        {
            textDisplay.text = line;
            textDisplay.alpha = 0f;
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                textDisplay.alpha = Mathf.Clamp01(timer / fadeDuration);
                yield return null;
            }
            textDisplay.alpha = 1f;
            while (!Input.GetMouseButtonDown(0)) yield return null;
            timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                textDisplay.alpha = Mathf.Clamp01(1 - (timer / fadeDuration));
                yield return null;
            }
            textDisplay.alpha = 0f;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}


