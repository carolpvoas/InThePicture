using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Fader : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public Text transitionText;
    public float fadeDuration = 1f;
    public float messageDuration = 2f;

    public static Fader Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            fadeCanvas.alpha = 0f;
            transitionText.text = "";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScene(string sceneName, string message = "")
    {
        StartCoroutine(FadeSceneTransition(sceneName, message));
    }

    private IEnumerator FadeSceneTransition(string sceneName, string message)
    {
        yield return StartCoroutine(Fade(1f));  // Fade to black
        
        transitionText.text = message; // Show message (opcional)
        yield return new WaitForSeconds(messageDuration);
        
        yield return SceneManager.LoadSceneAsync(sceneName); // Load new scene in background
        
        yield return null; // Espera 1 frame para garantir que a cena carregou
        
        transitionText.text = "";  // Esconde a mensagem e faz fade out
        yield return StartCoroutine(Fade(0f)); 
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvas.alpha;
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(startAlpha, targetAlpha, t / fadeDuration);
            yield return null;
        }

        fadeCanvas.alpha = targetAlpha;
    }
}