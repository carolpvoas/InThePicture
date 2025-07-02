using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class SceneTransitionManager : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    //public Text transitionText;  // ou TextMeshProUGUI se preferires
    public TextMeshProUGUI transitionText;
    public float fadeDuration = 1f;
    public float messageDuration = 2f;

    public static SceneTransitionManager Instance;

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
        yield return StartCoroutine(Fade(1f));

        transitionText.text = message;
        yield return new WaitForSeconds(messageDuration);

        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return null;

        transitionText.text = "";
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