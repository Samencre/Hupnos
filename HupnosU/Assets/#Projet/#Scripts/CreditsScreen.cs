using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour
{
float timer = 0.5f;
void Update()
{
    timer -= Time.deltaTime;
    if (timer > 0f) return;

    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
    {
        SceneManager.LoadScene("Start");
    }
}

}

